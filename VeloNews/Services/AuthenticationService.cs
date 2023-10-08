using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
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

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
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
            var user = _userRepository.GetUserWithProfileImage(id);

            return user;
        }

        public User GetUserByNameAndPass(string userName, string userPass)
        {
            return _userRepository.GetUserByNameAndPass(userName, userPass);
        }
        public User GetUserByName(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public bool IsAdmin()
        {
            var user = GetCurrentUser();

            if (user == null)
            {
                return false;
            }

            if (user.Name == DEFAULT_ADMIN_NAME)
            {
                return true;
            }

            return false;
        }
    }
}
