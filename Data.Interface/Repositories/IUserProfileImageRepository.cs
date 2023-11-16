using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserProfileImageRepository : IBaseRepository<UserProfileImage>
    {
        void SaveDefaultUserProfileImage(User user);
        void EditProfileImage(ProfileImageData data);
    }
}
