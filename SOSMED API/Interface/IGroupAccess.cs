using SOSMED_API.Models;

namespace SOSMED_API.Interface
{
    public interface IGroupAccess
    {
        List<GroupAccessModel> GetGroupAccessData();
        bool InsertGroupAccessData(IEnumerable<GroupAccessModel> _groupAccessModelList);
        bool DeleteGroupAccessData(string groupID);
    }
}
