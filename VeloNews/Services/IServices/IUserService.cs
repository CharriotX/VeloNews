using Data.Interface.Models;
using VeloNews.Models;

namespace VeloNews.Services.IServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetCurrentUser();
        User GetUserByNameAndPass(string userName, string userPass);
        User RegistrationUser(string name, string pass);
    }
}