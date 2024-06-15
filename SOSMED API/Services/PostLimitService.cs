using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Services
{
    public class PostLimitService : IPostLimit
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public PostLimitService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public PostLimitDataResponse GetPostLimitData()
        {
            var datalist = new List<PostLimitModel>();
            var response = new PostLimitDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select ROW_NUMBER() over (order by PostLimitID asc) as 'No', PostLimitID, PostLimitValue, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TPostLimit order by PostLimitID asc";

                    datalist = con.Query<PostLimitModel>(sql).AsList();
                 
                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get PostLimit data error!";
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

        public ResponseBaseModel InsertPostLimitData(PostLimitModel _postLimitModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Check is data exist
                    sql = @"select PostLimitID from TPostLimit where PostLimitID = @PostLimitID";
                    var existingData = con.QueryFirstOrDefault(sql, new { _postLimitModel.PostLimitID });

                    if (existingData != null)
                    {
                        response.IsSuccess = false;
                        response.Message = string.Format("Data already exist with PostLimit ID '{0}'!", _postLimitModel.PostLimitID);
                        return response;
                    }

                    // Insert data to database
                    sql = @"Insert into TPostLimit (PostLimitID, PostLimitValue, CreatedBy, CreatedDate)
                                values (@PostLimitID, @PostLimitValue, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _postLimitModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Insert data PostLimit failed!";
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

        public ResponseBaseModel UpdatePostLimitData(PostLimitModel _postLimitModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TPostLimit set PostLimitValue = @PostLimitValue, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where PostLimitID = @PostLimitID ";

                    var result = con.Execute(sql, _postLimitModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Update data PostLimit failed!";
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
        public ResponseBaseModel DeletePostLimitData(string postLimitID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TPostLimit where PostLimitID = @PostLimitID ";

                    var result = con.Execute(sql, new { PostLimitID = postLimitID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Delete data PostLimit failed!";
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
