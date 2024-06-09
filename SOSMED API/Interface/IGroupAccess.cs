using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IGroupAccess
    {
        GroupAccessDataResponse GetGroupAccessData();
        GroupAccessDataResponse GetGroupAccessDetailData(string groupID);
        ResponseBaseModel InsertGroupAccessData(IEnumerable<GroupAccessModel> _groupAccessModelList);
        ResponseBaseModel DeleteGroupAccessData(string groupID);
    }
}
