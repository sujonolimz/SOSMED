using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Models;

namespace SOSMED_API.Services
{
    public interface IPostingService
    {
        List<PostingModel> GetPostingData();
        //bool InsertPostingData(PostingModel _postingModel);
        //bool UpdatePostingData(PostingModel _postingModel);
        //bool DeletePostingData(string PostingID);
    }

    public class PostingService : IPostingService
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
    }
}
