using SOSMED_API.Models;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IAuth
    {
        LoginResponse Login(string userID, string password);
    }
}
