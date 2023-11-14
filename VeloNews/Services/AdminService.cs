using Data.Interface.Repositories;
using VeloNews.Models.AdminViewModels;
using VeloNews.Models.NewsViewModels;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;

namespace VeloNews.Services
{
    [AutoDiServiceRegistration]
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public AdminMainPageViewModel GetAdminMainPageViewModel()
        {
            var data = _adminRepository.GetDataFormAdminMainPage();

            var model = new AdminMainPageViewModel()
            {
                LastNews = data.LastPublishedNews.Select(x => new LastNewsViewModel()
                {
                    Id = x.Id,
                    Creator = x.Creator.AuthorName,
                    Title = x.Title
                }).ToList(),
                LastNewsComments = data.LastComments.Select(x => new LastNewsCommentsViewModel()
                {
                    Id = x.Id,
                    NewsId = x.NewsId,
                    Text = x.Text,
                    Creator = x.Creator.AuthorName
                }).ToList(),
                LastRegisteredUsers = data.LastRegisteredUsers.Select(x => new Models.UserViewModels.LastRegisteredUsersViewModel()
                {
                    Id = x.Id,
                    Name = x.UserName
                }).ToList(),
                SiteStats = new SiteStatsViewModel()
                {
                    NewsCount = data.SiteStats.NewsCount,
                    NewsCommentsCount = data.SiteStats.NewsCommentsCount,
                    UsersCount = data.SiteStats.UsersCount
                }
            };

            return model;
        }

        public List<NewsForAdminPageViewModel> GetNewsListForAdminPageViewModel()
        {
            var newsData = _adminRepository.GetAllNewsForAdminPage();

            var model = newsData.Select(x => new NewsForAdminPageViewModel
            {
                Id = x.Id,
                Title = x.Title,
                TimeOfCreation = x.TimeOfCreation,
                Creator = new UserInfoViewModel
                {
                    Id = x.Creator.Id,
                    Name = x.Creator.AuthorName
                }
            }).ToList();

            return model;
        }
    }
}
