using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Services
{
    public class PostingService : IPosting
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public PostingService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public PostingDataResponse GetPostingData()
        {
            var datalist = new List<PostingModel>();
            var response = new PostingDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select PostingID, Title, Description, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TPosting order by CreatedDate desc";

                    datalist = con.Query<PostingModel>(sql).AsList();

                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get Posting data error!";
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

        public ResponseBaseModel InsertPostingData(PostingModel _postingModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Check is data exist
                    sql = @"select Title from TPosting where Title = @Title and CreatedBy = @CreatedBy ";
                    var existingData = con.QueryFirstOrDefault(sql, new { _postingModel.Title, _postingModel.CreatedBy });

                    if (existingData != null)
                    {
                        response.IsSuccess = false;
                        response.Message = string.Format("Data already exist with Title '{0}'!", _postingModel.Title);
                        return response;
                    }

                    var isAllowCreatePost = CheckPostingLimit(_postingModel.CreatedBy);
                    if (isAllowCreatePost != null)
                    {
                        if (!isAllowCreatePost.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.Message = isAllowCreatePost.Message;
                            return response;
                        }
                    }

                    // Insert data to database
                    sql = @"Insert into TPosting (Title, Description, CreatedBy, CreatedDate)
                                values (@Title, @Description, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _postingModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Insert data Posting failed!";
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

        public ResponseBaseModel UpdatePostingData(PostingModel _postingModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TPosting set Title = @Title, Description = @Description, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where PostingID = @PostingID ";

                    var result = con.Execute(sql, _postingModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Update data Posting failed!";
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

        public ResponseBaseModel DeletePostingData(string postingID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TPosting where PostingID = @PostingID ";

                    var result = con.Execute(sql, new { PostingID = postingID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Delete data Posting failed!";
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

        public ResponseBaseModel CheckPostingLimit(string userID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Check is allow create new post
                    sql = @"declare @isAllowPost bit = 0
                            declare @totalPosting int =
                            ( 
                            select COUNT(PostingID) as totalPosting from TPosting
                            where CreatedBy = @CreatedBy
                            )

                            declare @totalLimit int =
                            (
                            select T2.PostLimitValue from TUser T1
                            inner join TPostLimit T2 on T1.PostLimitID = T2.PostLimitID
                            where UserID = @CreatedBy
                            )

                            if(@totalPosting = @totalLimit)
                            begin
	                            select @isAllowPost 
                            end
                            else begin
	                            set @isAllowPost = 1
	                            select @isAllowPost
                            end ";

                    var isAllowCreatePost = con.ExecuteScalar(sql, new { CreatedBy = userID });

                    if (!Convert.ToBoolean(isAllowCreatePost))
                    {
                        response.IsSuccess = false;
                        response.Message = "Can't create new post, already reach the limit. please contact the admin";
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
