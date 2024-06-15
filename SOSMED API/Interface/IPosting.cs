using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IPosting
    {
        PostingDataResponse GetPostingData(string userID);
        ResponseBaseModel InsertPostingData(PostingModel _postingModel);
        ResponseBaseModel UpdatePostingData(PostingModel _postingModel);
        ResponseBaseModel DeletePostingData(string PostingID);
    }
}
