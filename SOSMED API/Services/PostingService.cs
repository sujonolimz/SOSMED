using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Services
{
    public class PostingService : IPosting
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public PostingService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public List<PostingModel> GetPostingData()
        {
            var datalist = new List<PostingModel>();

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select PostingID, Title, Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TPosting";

                    datalist = con.Query<PostingModel>(sql).AsList();
                    return datalist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertPostingData(PostingModel _postingModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"Insert into TPosting (Title, Description, CreatedBy, CreatedDate)
                                values (@Title, @Description, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _postingModel);

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

        public bool UpdatePostingData(PostingModel _postingModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TPosting set Title = @Title, Description = @Description, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where PostingID = @PostingID ";

                    var result = con.Execute(sql, _postingModel);

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

        public bool DeletePostingData(string postingID)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TPosting where PostingID = @PostingID ";

                    var result = con.Execute(sql, new { PostingID = postingID });

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
