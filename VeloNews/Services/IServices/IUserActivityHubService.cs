using Data.Interface.Models.enums;
using VeloNews.Models.UserActivityViewModels;

namespace VeloNews.Services.IServices
{
    public interface IUserActivityHubService
    {
        void SaveUserCommentActivityHistory(string username, string newsId, string text);
        void UserLogin(int userId, string username);
        void UserLogout();
        List<UserActivityViewModel> GetAllActions();
    }
}
