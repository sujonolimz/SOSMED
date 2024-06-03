using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Models;

namespace SOSMED_API.Services
{
    public class GroupAccessService : IGroupAccess
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public GroupAccessService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public List<GroupAccessModel> GetGroupAccessData()
        {
            var datalist = new List<GroupAccessModel>();

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select GroupID, FormID, CreatedBy, CreatedDate from TGroupAccess";

                    datalist = con.Query<GroupAccessModel>(sql).AsList();
                    return datalist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertGroupAccessData(IEnumerable<GroupAccessModel> _groupAccessModelList)
        {
            bool success = false;

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

        public bool DeleteGroupAccessData(string groupID)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TGroupAccess where GroupID = @GroupID ";

                    var result = con.Execute(sql, new { GroupID = groupID });

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
