using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IGroup
    {
        GroupDataResponse GetGroupData();
        ResponseBaseModel InsertGroupData(GroupModel _groupModel);
        ResponseBaseModel UpdateGroupData(GroupModel _groupModel);
        ResponseBaseModel DeleteGroupData(string groupID);
    }
}
