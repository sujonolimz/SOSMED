using SOSMED_API.Models;

namespace SOSMED_API.Interface
{
    public interface IGroup
    {
        List<GroupModel> GetGroupData();
        bool InsertGroupData(GroupModel _groupModel);
        bool UpdateGroupData(GroupModel _groupModel);
        bool DeleteGroupData(string groupID);
    }
}
