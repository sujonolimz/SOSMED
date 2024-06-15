using SOSMED_API.Models.Commons;

namespace SOSMED_API.Models
{
    public class UserModel : CommonModel
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? GroupID { get; set; }
        public string? PostLimitID { get; set; }
        public string? Dept { get; set; }
    }

    public class MAUsModel : ResponseBaseModel
    {
        public int TotalMAUs { get; set; }
    }

    public class LoginHistoryModel
    {
        public int? No { get; set; }
        public string ID { get; set;}
        public string UserID { get; set;}
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
