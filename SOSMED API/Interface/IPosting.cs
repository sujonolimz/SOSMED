using SOSMED_API.Models;

namespace SOSMED_API.Interface
{
    public interface IPosting
    {
        List<PostingModel> GetPostingData();
        bool InsertPostingData(PostingModel _postingModel);
        bool UpdatePostingData(PostingModel _postingModel);
        bool DeletePostingData(string PostingID);
    }
}
