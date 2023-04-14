using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models;

namespace VeloNews.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
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
            var user = _userRepository.Get(id);

            return user;
        }

        public User GetUserByNameAndPass(string userName, string userPass)
        {
            return _userRepository.GetUserByNameAndPass(userName, userPass);
        }

        public User RegistrationUser(string name, string pass)
        {
            var user = new User()
            {
                Name = name,
                Password = pass
            };

            _userRepository.Save(user);

            return user;
        }
    }
}
