using SOSMED_API.Models.Commons;

namespace SOSMED_API.Models
{
    public class PostingModel : CommonModel
    {
        public int PostingID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
