using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Data.Sql.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WebContext webContext) : base(webContext)
        {

        }

        public List<LastRegisteredUser> GetLastRegisteredUsers()
        {
            var lastRegisterUser = _dbSet.OrderByDescending(x => x.Id).Take(10).ToList();
            var data = lastRegisterUser.Select(x => new LastRegisteredUser
            {
                Id = x.Id,
                UserName = x.Name
            }).ToList();

            return data;
        }

        public User GetUserByNameAndPass(string name, string pass)
        {
            return _dbSet.SingleOrDefault(x => x.Name == name && x.Password == pass);
        }
        public User GetUserByUserName(string userName)
        {
            return _dbSet.FirstOrDefault(x => x.Name == userName);
        }
        public bool IsUserExist(string userName)
        {
            return _dbSet.Any(x => x.Name == userName);
        }

        public bool IsUserNameExist(string userName)
        {
            return _dbSet.Any(x => x.Name == userName);
        }

        public User GetUserWithProfileImage(int userId)
        {
            var user = _dbSet.Include(x => x.UserProfileImage).SingleOrDefault(x => x.Id == userId);
            return user;
        }

        public User UserRegistration(UserRegistrationData data)
        {
            var model = new User()
            {
                Name = data.UserName,
                Password = data.Password,
                Role = UserRole.User,
                DateOfBirth = new DateTime(1500, 01, 01),
                Country = "Не указан",
                UserCreationDate = DateTime.Now
            };

            _dbSet.Add(model);
            _webContext.SaveChanges();

            return model;
        }

        public ShowUserProfileData GetUserProfileData(int userId)
        {
            var user = GetUserWithProfileImage(userId);

            var data = new ShowUserProfileData
            {
                User = new UserData
                {
                    Id = user.Id,
                    UserProfileImageUrl = user.UserProfileImage.Url,
                    UserName = user.Name,
                    Role = user.Role.ToString(),
                    Country = user.Country,
                    UserCreationDate = user.UserCreationDate,
                    DateOfBirth = user.DateOfBirth
                }
            };

            return data;
        }

        public void EditMyProfile(EditMyProfileData data)
        {
            var user = Get(data.Id);
            user.Name = data.Name;
            user.Country = data.Country;
            user.DateOfBirth = data.DateOfBirth;

            _webContext.SaveChanges();
        }
    }
}
