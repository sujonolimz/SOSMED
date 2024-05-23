using Dapper;
using SOSMED_API.Helpers;
using SOSMED_API.Models;

namespace SOSMED_API.Services
{
    public interface IGroupService
    {
        List<GroupModel> GetGroupData();
        bool InsertGroupData(GroupModel _groupModel);
        bool UpdateGroupData(GroupModel _groupModel);
        bool DeleteGroupData(string groupID);
    }

    public class GroupService : IGroupService
    {
        private readonly SqlServerConnector _sqlserverconnector;

        public GroupService(SqlServerConnector sqlServerConnector)
        {
            _sqlserverconnector = sqlServerConnector;
        }

        public List<GroupModel> GetGroupData()
        {
            var datalist = new List<GroupModel>();

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = "Select GroupID, GroupDesc, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from TGroup";

                    datalist = con.Query<GroupModel>(sql).AsList();
                    return datalist;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertGroupData(GroupModel _groupModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"Insert into TGroup (GroupID, GroupDesc, CreatedBy, CreatedDate)
                                values (@GroupID, @GroupDesc, @CreatedBy, CURRENT_TIMESTAMP) ";

                    var result = con.Execute(sql, _groupModel);

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

        public bool UpdateGroupData(GroupModel _groupModel)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"update TGroup set GroupDesc = @GroupDesc, UpdatedBy = @UpdatedBy, UpdatedDate = CURRENT_TIMESTAMP where GroupID = @GroupID ";

                    var result = con.Execute(sql, _groupModel);

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

        public bool DeleteGroupData(string groupID)
        {
            bool success = false;

            try
            {
                using (var con = _sqlserverconnector.GetConnection())
                {
                    con.Open();
                    string sql = @"delete from TGroup where GroupID = @GroupID ";

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
