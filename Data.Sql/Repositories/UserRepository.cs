using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WebContext webContext) : base(webContext)
        {
        }

        public User GetUserByNameAndPass(string name, string pass)
        {
            return _dbSet.SingleOrDefault(x => x.Name == name && x.Password == pass);
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
