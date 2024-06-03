
using SOSMED_API.Models.Commons;

namespace SOSMED_API.Models
{
    public class LoginModel
    {
        public required string UserID { get; set; }
        public required string Password { get; set; }
    }

    public class UserLoginDataModel
    {
        public string UserID { get; set;}
        public string UserName { get; set; }
        public string GroupID { get; set; }
        public string Dept { get; set; }
        public string Token { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}
