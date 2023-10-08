using Data.Interface.Repositories;
using Microsoft.AspNetCore.SignalR;
using VeloNews.Models.UserActivityViewModels;
using VeloNews.Services.IServices;
using VeloNews.SignalRHubs;

namespace VeloNews.Services
{
    public class UserActivityHubService : IUserActivityHubService
    {
        private IHubContext<AdminUserActivityHub> _hubContext;
        private IUserActivityRepository _userActivityRepository;
        private IAuthenticationService _authenticationService;

        public UserActivityHubService(IHubContext<AdminUserActivityHub> hubContext,
            IUserActivityRepository userActivityRepository,
            IAuthenticationService authenticationService)
        {
            _hubContext = hubContext;
            _userActivityRepository = userActivityRepository;
            _authenticationService = authenticationService;
        }


        public void SaveUserCommentActivityHistory(string username, string newsId, string text)
        {
            _hubContext
                .Clients
                .All
                .SendAsync("AddNewComment", username, newsId, text);

            _userActivityRepository.SaveCommentActivityHistory(username, newsId, text);
        }

        public void UserLogin(int userId, string username)
        {
            _hubContext
                .Clients
                .All
                .SendAsync("LogIn", userId.ToString(), username);
            var description = "зашел в учетную запись.";

            _userActivityRepository.SaveAutorizationActivityHistory(username, description);
        }

        public void UserLogout()
        {
            var user = _authenticationService.GetCurrentUser();

            _hubContext
                .Clients
                .All
                .SendAsync("LoggedOut", user.Id.ToString(), user.Name);
            var description = "вышел из учетной записи.";

            _userActivityRepository.SaveAutorizationActivityHistory(user.Name, description);
        }

        public List<UserActivityViewModel> GetAllActions()
        {
            var data = _userActivityRepository.GetUserActivityHistory();

            var viewModels = data.Select(x => new UserActivityViewModel()
            {
                Id = x.Id,
                UserName = x.Username,
                Description = x.Description
            }).ToList();

            return viewModels;
        }
    }
}
