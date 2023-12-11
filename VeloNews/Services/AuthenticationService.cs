using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;

namespace VeloNews.Services
{
    [AutoDiServiceRegistration]
    public class AuthenticationService : IAuthenticationService
    {
        private const string DEFAULT_ADMIN_NAME = "admin";

        private IUserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<UserApiData> GetAllUsersForApi()
        {
            var users = _userRepository.GetAllUsers();

            var data = users.Select(x => new UserApiData
            {
                Name = x.Name,
                Country = x.Country,
                DateOfBirth = x.DateOfBirth,
                Language = x.Language.ToString()
            }).ToList();

            return data;
        }

        public UserData GetCurrentUserData()
        {
            var idStr = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(x => x.Type == "Id")
                ?.Value;

            if (idStr == null)
            {
                return null;
            }

            var id = int.Parse(idStr);
            var user = _userRepository.GetUserById(id);

            var data = new UserData
            {
                Id = user.Id,
                Name = user.Name,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Language = user.Language,
                Role = user.Role.ToString(),
                UserCreationDate = user.UserCreationDate,
                UserProfileImageUrl = user.UserProfileImage.Url
            };

            return data;
        }

        public User GetCurrentUser()
        {
            var idStr = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(x => x.Type == "Id")
                ?.Value;

            if (idStr == null)
            {
                return null;
            }

            var id = int.Parse(idStr);
            var user = _userRepository.GetUserById(id);

            return user;
        }

        public UserData GetUserDataByNameAndPass(string userName, string userPass)
        {
            var user = _userRepository.GetUserByNameAndPass(userName, userPass);

            var data = new UserData
            {
                Id = user.Id,
                Name = user.Name,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Language = user.Language,
                Role = user.Role.ToString(),
                UserCreationDate = user.UserCreationDate,
                UserProfileImageUrl = user.UserProfileImage.Url
            };
            return data;
        }
        public UserData GetUserByName(string username)
        {
            var user = _userRepository.GetUserByUsername(username);

            var data = new UserData
            {
                Id = user.Id,
                Name = user.Name,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Language = user.Language,
                Role = user.Role.ToString(),
                UserCreationDate = user.UserCreationDate,
                UserProfileImageUrl = user.UserProfileImage.Url
            };

            return data;
        }

        public bool IsAdmin()
        {
            var user = GetCurrentUserData();

            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.Admin.ToString())
            {
                return true;
            }

            return false;
        }

        public bool IsNewsModerator()
        {
            var user = GetCurrentUserData();

            if (user == null)
            {
                return false;
            }

            if (user.Role == UserRole.NewsModerator.ToString())
            {
                return true;
            }

            return false;
        }

        public bool IsUserAuthorized()
        {
            var user = GetCurrentUserData();

            return user == null ? false : true;
        }

        public UserApiData GetUserForApi(int id)
        {
            var user = _userRepository.Get(id);

            var data = new UserApiData
            {
                Name = user.Name,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Language = user.Language.ToString()
            };

            return data;
        }
    }
}
