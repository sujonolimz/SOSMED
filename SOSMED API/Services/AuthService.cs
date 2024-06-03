using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Services
{
    public class AuthService : IAuth
    {
        private readonly SqlServerConnector _sqlserverconnector;
        private readonly TokenService _tokenService;

        public AuthService(SqlServerConnector sqlServerConnector, TokenService tokenService)
        {
            _sqlserverconnector = sqlServerConnector;
            _tokenService = tokenService;
        }

        public LoginResponse Login(string userID, string password)
        {
            var response = new LoginResponse();

            try
            {
                // Check is user login data valid
                response = CheckLogin(userID, password);

                if (response.IsSuccess)
                {
                    // Generate Token
                    var token = _tokenService.GenerateToken(response.UserData);
                    response.UserData.Token = token;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public LoginResponse CheckLogin(string userID, string password)
        {
            var response = new LoginResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select UserID, UserName, Dept, GroupID from TUser where UserID = @UserID and PWDCOMPARE(@Password,Password) = 1 ";

                    var userData = con.QueryFirstOrDefault<UserLoginDataModel>(sql, new { UserID = userID, Password = password });

                    if (userData != null)
                    {
                        response.UserData = userData;
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Invalid user ID or password";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

    }
}
