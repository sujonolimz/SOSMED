using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IPostLimit
    {
        PostLimitDataResponse GetPostLimitData();
        ResponseBaseModel InsertPostLimitData(PostLimitModel _postLimitModel);
        ResponseBaseModel UpdatePostLimitData(PostLimitModel _postLimitModel);
        ResponseBaseModel DeletePostLimitData(string postLimitID);
    }
}
