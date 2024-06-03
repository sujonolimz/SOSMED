using SOSMED_API.Models.Commons;

namespace SOSMED_API.Models.Responses
{
    public class ResponseModel
    {
        public class LoginResponse : ResponseBaseModel
        {
            public UserLoginDataModel UserData { get; set; }
        }
    }
}
