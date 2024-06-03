using SOSMED_API.Models;

namespace SOSMED_API.Interface
{
    public interface IUser
    {
        List<UserModel> GetUserData();
        bool InsertUserData(UserModel _userModel);
        bool UpdateUserData(UserModel _userModel);
        bool DeleteUserData(string userID);
    }
}
