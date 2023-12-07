using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByNameAndPass(string name, string pass);
        List<User> GetAllUsers();
        bool IsUserExist(string userName);
        List<LastRegisteredUserData> GetLastRegisteredUsers();
        User UserRegistration(UserRegistrationData data);
        User GetUserByUsername(string userName);
        bool IsUserNameExist(string userName);
        User GetUserById(int userId);
        ShowMyProfileData GetUserProfileData(int userId);
        void EditMyProfile(EditMyProfileData data);
    }
}
