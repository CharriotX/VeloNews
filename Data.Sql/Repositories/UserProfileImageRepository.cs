using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Sql.Repositories
{
    public class UserProfileImageRepository : BaseRepository<UserProfileImage>, IUserProfileImageRepository
    {
        private IUserRepository _userRepository;
        public UserProfileImageRepository(WebContext webContext, IUserRepository userRepository) : base(webContext)
        {
            _userRepository = userRepository;
        }

        public void SaveDefaultUserProfileImage(User user)
        {
            _dbSet.Add(new UserProfileImage()
            {
                Name = "defaultUserImage",
                Url = $"/images/default-user-image.png",
                User = user
            });

            _webContext.SaveChanges();
        }

        public void EditProfileImage(ProfileImageData data)
        {
            var user = _userRepository.GetUserWithProfileImage(data.UserId);

            user.UserProfileImage.Name = data.Name;
            user.UserProfileImage.Url = data.Url;

            _webContext.SaveChanges();
        }
    }
}
