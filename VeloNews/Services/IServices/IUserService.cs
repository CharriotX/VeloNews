using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using VeloNews.Models;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Services.IServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetCurrentUser();
        User GetUserByNameAndPass(string userName, string userPass);
        void RegistrationUser(RegistrationUserViewModel viewModel);
    }
}