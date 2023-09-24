using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByNameAndPass(string name, string pass);
        bool IsUserExist(string userName);
        List<LastRegisteredUser> GetLastRegisteredUsers();
        User UserRegistration(UserRegistrationData data);
        User GetUserByUserName(string userName);
        bool IsUserNameExist(string userName);
        User GetUserWithProfileImage(int userId);
        ShowUserProfileData GetUserProfileData(int userId);
        void EditMyProfile(EditMyProfileData data);
    }
}
