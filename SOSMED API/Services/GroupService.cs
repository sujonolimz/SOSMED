using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Services
{
    public class GroupService : IGroup
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public GroupService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public GroupDataResponse GetGroupData()
        {
            var datalist = new List<GroupModel>();
            var response = new GroupDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select ROW_NUMBER() over (order by GroupID asc) as 'No', GroupID, GroupDesc, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TGroup order by GroupID asc";

                    datalist = con.Query<GroupModel>(sql).AsList();

                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get Group data error!";
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

        public ResponseBaseModel InsertGroupData(GroupModel _groupModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";

                    // Check is data exist
                    sql = @"select GroupID from TGroup where GroupID = @GroupID";
                    var existingData = con.QueryFirstOrDefault(sql, new { _groupModel.GroupID });

                    if (existingData != null)
                    {
                        response.IsSuccess = false;
                        response.Message = string.Format("Data already exist with Group ID '{0}'!", _groupModel.GroupID);
                        return response;
                    }

                    // Insert data to database
                    sql = @"Insert into TGroup (GroupID, GroupDesc, CreatedBy, CreatedDate)
                                values (@GroupID, @GroupDesc, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _groupModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Insert data Group failed!";
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

        public ResponseBaseModel UpdateGroupData(GroupModel _groupModel)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TGroup set GroupDesc = @GroupDesc, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where GroupID = @GroupID ";

                    var result = con.Execute(sql, _groupModel);

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Update data Group failed!";
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

        public ResponseBaseModel DeleteGroupData(string groupID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TGroup where GroupID = @GroupID ";

                    var result = con.Execute(sql, new { GroupID = groupID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Delete data Group failed!";
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
