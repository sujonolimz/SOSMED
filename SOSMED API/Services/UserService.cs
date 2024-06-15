using Dapper;
using Microsoft.IdentityModel.Tokens;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Services
{
    public class UserService : IUser
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public UserService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public UserDataResponse GetUserData()
        {
            var datalist = new List<UserModel>();
            var response = new UserDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select ROW_NUMBER() over (order by UserID asc) as 'No', UserID, UserName, Dept, GroupID, PostLimitID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TUser order by UserID asc";

                    datalist = con.Query<UserModel>(sql).AsList();
                  
                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get User data error!";
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

        public ResponseBaseModel InsertUserData(UserModel _userModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Check is data exist
                    sql = @"select UserID from TUser where UserID = @UserID";
                    var existingData = con.QueryFirstOrDefault(sql, new { _userModel.UserID });

                    if (existingData != null)
                    {
                        response.IsSuccess = false;
                        response.Message = string.Format("Data already exist with User ID '{0}'!", _userModel.UserID);
                        return response;
                    }

                    // Insert data to database
                    sql = @"Insert into TUser (UserID, UserName, Password, Dept, GroupID, PostLimitID, CreatedBy, CreatedDate)
                                values (@UserID, @UserName, PWDENCRYPT(@Password), @Dept, @GroupID, @PostLimitID, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _userModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Insert data User failed!";
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

        public ResponseBaseModel UpdateUserData(UserModel _userModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TUser set UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP ";

                    #region Update specific data conditions
                    if (!_userModel.UserName.IsNullOrEmpty())
                    {
                        sql += ", UserName = @UserName ";
                    }

                    if (!_userModel.Password.IsNullOrEmpty())
                    {
                        sql += ", Password = pwdencrypt(@Password) ";
                    }

                    if (!_userModel.Dept.IsNullOrEmpty())
                    {
                        sql += ", Dept = @Dept ";
                    }

                    if (!_userModel.GroupID.IsNullOrEmpty())
                    {
                        sql += ", GroupID = @GroupID ";
                    }

                    if (!_userModel.PostLimitID.IsNullOrEmpty())
                    {
                        sql += ", PostLimitID = @PostLimitID ";
                    }
                    #endregion
                    sql += " where UserID = @UserID ";

                    var result = con.Execute(sql, _userModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Update data User failed!";
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

        public ResponseBaseModel DeleteUserData(string userID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TUser where UserID = @UserID ";

                    var result = con.Execute(sql, new { UserID = userID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Delete data User failed!";
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

        public MAUsModel GetTotalMAUs()
        {
            var response = new MAUsModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @";with cte as (
                    select COUNT(T1.UserID) as total_login, T1.UserID from TLoginHistory T1
                    inner join TUser T2 on T1.UserID = T2.UserID
                    where YEAR(T1.LoginTime) = YEAR(GETDATE())
                    and MONTH(T1.LoginTime) = MONTH(GETDATE())
                    group by T1.UserID
                    )
                    select COUNT(UserID) as TotalMAUs from cte ";

                    var totalData = con.ExecuteScalar(sql);

                    if (totalData != null)
                    {
                        response.IsSuccess = true;
                        response.TotalMAUs = Convert.ToInt32(totalData);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "GetTotalMAUs data error!";
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

        public UserLoginHistoryDataResponse GetUserLoginHistoryData(string year, string month)
        {
            var datalist = new List<LoginHistoryModel>();
            var response = new UserLoginHistoryDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"
                    select ROW_NUMBER() over (order by T1.LoginTime desc) as 'No', T1.ID, T1.UserID, T2.UserName, T1.LoginTime from TLoginHistory T1
                    inner join TUser T2 on T1.UserID = T2.UserID 
                    where YEAR(T1.LoginTime) = @year and MONTH(T1.LoginTime) = @month 
                    order by T1.LoginTime desc ";

                    datalist = con.Query<LoginHistoryModel>(sql, new {year = year, month = month}).AsList();

                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get User Login History data error!";
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
