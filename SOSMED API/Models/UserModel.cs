namespace SOSMED_API.Models
{
    public class UserModel : CommonModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GroupID { get; set; }
        public string PostLimitID { get; set; }
        public string Dept { get; set; }
    }
}
