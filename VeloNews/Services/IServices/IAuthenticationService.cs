using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;

namespace VeloNews.Services.IServices
{
    public interface IAuthenticationService
    {
        List<UserApiData> GetAllUsersForApi();
        UserApiData GetUserForApi(int id);
        UserData GetCurrentUser();
        UserData GetUserByNameAndPass(string userName, string userPass);
        UserData GetUserByName(string username);
        bool IsAdmin();
        public bool IsUserAuthorized();
    }
}
