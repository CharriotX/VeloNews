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

        public void UserRegistration(UserRegistrationData data)
        {
            var model = new User()
            {
                Name = data.UserName,
                Password = data.Password
            };

            _dbSet.Add(model);
            _webContext.SaveChanges();
        }
    }
}
