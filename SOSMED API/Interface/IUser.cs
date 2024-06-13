using SOSMED_API.Models;
using SOSMED_API.Models.Commons;
using static SOSMED_API.Models.Responses.ResponseModel;

namespace SOSMED_API.Interface
{
    public interface IUser
    {
        UserDataResponse GetUserData();
        ResponseBaseModel InsertUserData(UserModel _userModel);
        ResponseBaseModel UpdateUserData(UserModel _userModel);
        ResponseBaseModel DeleteUserData(string userID);
        MAUsModel GetTotalMAUs();
    }
}
