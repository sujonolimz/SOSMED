using SOSMED_API.Models;

namespace SOSMED_API.Interface
{
    public interface IPostLimit
    {
        List<PostLimitModel> GetPostLimitData();
        bool InsertPostLimitData(PostLimitModel _postLimitModel);
        bool UpdatePostLimitData(PostLimitModel _postLimitModel);
        bool DeletePostLimitData(string postLimitID);
    }
}
