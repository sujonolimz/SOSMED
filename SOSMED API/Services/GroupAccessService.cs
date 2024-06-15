using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;


namespace SOSMED_API.Services
{
    public class GroupAccessService : IGroupAccess
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public GroupAccessService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public GroupAccessDataResponse GetGroupAccessData()
        {
            var datalist = new List<GroupAccessModel>();
            var response = new GroupAccessDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "";
                    //string sql = "Select GroupID, FormID, CreatedBy, CreatedDate from TGroupAccess order by GroupID asc";

                    sql = @" 
                    ;WITH cte AS (
                        SELECT 
                            T1.GroupID, 
		                    T2.GroupDesc,
		                    T1.CreatedBy,
                            T1.CreatedDate,
                            ROW_NUMBER() OVER (PARTITION BY T1.GroupID ORDER BY T1.CreatedDate desc) AS rn
                        FROM TGroupAccess T1 
                    inner join TGroup T2 on T1.GroupID = T2.GroupID
                    )
                    SELECT 
                        ROW_NUMBER() over (order by GroupID asc) as 'No',
                        GroupID,
	                    GroupDesc,
	                    CreatedBy,
	                    CreatedDate
                    FROM cte
                    WHERE rn = 1 order by GroupID; ";

                    datalist = con.Query<GroupAccessModel>(sql).AsList();

                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get Group Access data error!";
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

        public GroupAccessDataResponse GetGroupAccessDetailData(string groupID)
        {
            var datalist = new List<GroupAccessModel>();
            var response = new GroupAccessDataResponse();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select GroupID, FormID from TGroupAccess where GroupID = @GroupID order by GroupID asc";

                    datalist = con.Query<GroupAccessModel>(sql, new { GroupID = groupID }).AsList();

                    if (datalist != null)
                    {
                        response.IsSuccess = true;
                        response.Content = datalist;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "get Group Access data error!";
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

        public ResponseBaseModel InsertGroupAccessData(IEnumerable<GroupAccessModel> _groupAccessModelList)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    
                    using (var conTrans = con.BeginTransaction())
                    {
                        // Delete all existing data based on GroupID
                        string sqlDelete = "delete from TGroupAccess where GroupID = @GroupID";
                        con.Execute(sqlDelete, new { GroupID = _groupAccessModelList.First().GroupID }, transaction: conTrans);

                        // Insert new data to TGroupAccess
                        string sql = @"Insert into TGroupAccess (GroupID, FormID, CreatedBy, CreatedDate)
                                values (@GroupID, @FormID, @CreatedBy, CURRENT_TIMESTAMP) ";

                        foreach (var data in _groupAccessModelList)
                        {
                            con.Execute(sql, data, transaction: conTrans);
                        }
                        conTrans.Commit();
                        response.IsSuccess = true;
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

        public ResponseBaseModel DeleteGroupAccessData(string groupID)
        {
            var response = new ResponseBaseModel();
            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TGroupAccess where GroupID = @GroupID ";

                    var result = con.Execute(sql, new { GroupID = groupID });

                    if (result > 0)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Delete data Group Access failed!";
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
