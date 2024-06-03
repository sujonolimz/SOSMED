using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Services
{
    public class PostLimitService : IPostLimit
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public PostLimitService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public List<PostLimitModel> GetPostLimitData()
        {
            var datalist = new List<PostLimitModel>();

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select PostLimitID, PostLimitValue, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TPostLimit";

                    datalist = con.Query<PostLimitModel>(sql).AsList();
                    return datalist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertPostLimitData(PostLimitModel _postLimitModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"Insert into TPostLimit (PostLimitID, PostLimitValue, CreatedBy, CreatedDate)
                                values (@PostLimitID, @PostLimitValue, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _postLimitModel);

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

        public bool UpdatePostLimitData(PostLimitModel _postLimitModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TPostLimit set PostLimitValue = @PostLimitValue, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where PostLimitID = @PostLimitID ";

                    var result = con.Execute(sql, _postLimitModel);

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
        public bool DeletePostLimitData(string postLimitID)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TPostLimit where PostLimitID = @PostLimitID ";

                    var result = con.Execute(sql, new { PostLimitID = postLimitID });

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
