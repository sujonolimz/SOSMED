using Dapper;
using Microsoft.IdentityModel.Tokens;
using SOSMED_API.Helpers;
using SOSMED_API.Models;

namespace SOSMED_API.Services
{
    public interface IUserService
    {
        List<UserModel> GetUserData();
        bool InsertUserData(UserModel _userModel);
        bool UpdateUserData(UserModel _userModel);
        bool DeleteUserData(string userID);
    }

    public class UserService : IUserService
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public UserService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public List<UserModel> GetUserData()
        {
            var datalist = new List<UserModel>();

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select UserID, UserName, Dept, GroupID, PostLimitID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TUser";

                    datalist = con.Query<UserModel>(sql).AsList();
                    return datalist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertUserData(UserModel _userModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"Insert into TUser (UserID, UserName, Password, Dept, GroupID, PostLimitID, CreatedBy, CreatedDate)
                                values (@UserID, @UserName, PWDENCRYPT(@Password), @Dept, @GroupID, @PostLimitID, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _userModel);

                    if (result > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return success;
        }

        public bool UpdateUserData(UserModel _userModel)
        {
            bool success = false;

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
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return success;
        }

        public bool DeleteUserData(string userID)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TUser where UserID = @UserID ";

                    var result = con.Execute(sql, new { UserID = userID });

                    if (result > 0)
                    {
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return success;
        }
    }
}
