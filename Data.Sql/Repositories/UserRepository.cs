using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        const string DEFAULT_ADMIN_USERNAME = "admin";
        public UserRepository(WebContext webContext) : base(webContext)
        {

        }

        public List<LastRegisteredUserData> GetLastRegisteredUsers()
        {
            var lastRegisterUser = _dbSet.OrderByDescending(x => x.Id).Take(10).ToList();

            var data = lastRegisterUser.Select(x => new LastRegisteredUserData
            {
                Id = x.Id,
                UserName = x.Name
            }).ToList();

            return data;
        }

        public List<UserDataForLogin> GetUsersForLogin()
        {
            var users = _dbSet
                .Take(3)
                .ToList();

            if (!users.Contains(GetUserByUsername(DEFAULT_ADMIN_USERNAME)))
            {
                var admin = GetUserByUsername(DEFAULT_ADMIN_USERNAME);
                users.Add(admin);
            }

            var data = users.Select(x => new UserDataForLogin
            {
                Username = x.Name,
                Password = x.Password,
                Role = x.Role.ToString()
            })
                .ToList();

            return data;
        }

        public List<User> GetAllUsers()
        {
            return _dbSet
                .Include(x => x.UserProfileImage)
                .ToList();
        }

        public User GetUserByNameAndPass(string name, string pass)
        {
            return _dbSet
                .Include(x => x.UserProfileImage)
                .SingleOrDefault(x => x.Name == name && x.Password == pass);
        }
        public User GetUserByUsername(string userName)
        {
            return _dbSet
                .Include(x => x.UserProfileImage)
                .SingleOrDefault(x => x.Name == userName);
        }
        public bool IsUserExist(string userName)
        {
            return _dbSet.Any(x => x.Name == userName);
        }

        public bool IsUserNameExist(string userName)
        {
            return _dbSet.Any(x => x.Name == userName);
        }

        public User GetUserById(int userId)
        {
            return _dbSet
                .Include(x => x.UserProfileImage)
                .SingleOrDefault(x => x.Id == userId);
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

        public ShowMyProfileData GetUserProfileData(int userId)
        {
            var user = GetUserById(userId);

            var data = new ShowMyProfileData
            {
                User = new UserData
                {
                    Id = user.Id,
                    UserProfileImageUrl = user.UserProfileImage.Url,
                    Name = user.Name,
                    Role = user.Role.ToString(),
                    Country = user.Country,
                    UserCreationDate = user.UserCreationDate,
                    DateOfBirth = user.DateOfBirth,
                    Language = user.Language
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
            user.Language = data.Language;

            _webContext.SaveChanges();
        }
    }
}
