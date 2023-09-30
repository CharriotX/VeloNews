using Data.Interface.Models;

namespace VeloNews.Services.IServices
{
    public interface IAuthenticationService
    {
        List<User> GetAllUsers();
        User GetCurrentUser();
        User GetUserByNameAndPass(string userName, string userPass);
        User GetUserByName(string username);
        bool IsAdmin();
    }
}
