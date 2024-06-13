using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
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

                    // Insert Login history
                    var LoginHistory = InsertUserLoginHistoryData(userID);
                    if (!LoginHistory.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.Message = LoginHistory.Message;
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
                        response.Message = "Invalid User ID or Password";
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

        public ResponseBaseModel InsertUserLoginHistoryData(string userID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Insert data to database
                    sql = @"Insert into TLoginHistory (UserID, LoginTime) values(@UserID, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, new { UserID = userID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Insert data User Login history failed!";
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

        public ResponseBaseModel isHaveFormAccess(CheckIsHaveAccess model)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    sql = @"declare @groupID nvarchar(50) = (select GroupID from TUser where UserID = @UserID) 
                    select GroupID, FormID from TGroupAccess where GroupID = @groupID and FormID = @FormID ";

                    var existingData = con.QueryFirstOrDefault(sql, new { UserID = model.UserID, FormID = model.FormID });

                    if (existingData == null)
                    {
                        response.IsSuccess = false;
                        response.Message = string.Format("Your user id not have access to form '{0}'!", model.FormID);
                        return response;
                    }

                    response.IsSuccess = true;
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
