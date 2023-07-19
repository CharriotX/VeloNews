﻿using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models;
using VeloNews.Models.AdminViewModels;
using VeloNews.Models.NewsViewModels;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.Helpers;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class AdminService : IAdminService
    {
        private IUserService _userService;
        private IAdminRepository _adminRepository;
        
        public AdminService(IUserService userService, IAdminRepository adminRepository)
        {
            _userService = userService;
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
                    Creator = x.Creator.Name,
                    Title = x.Title
                }).ToList(),
                LastNewsComments = data.LastComments.Select(x => new LastNewsCommentsViewModel()
                {
                    Id = x.Id,
                    NewsId = x.NewsId,
                    Text = x.Text,
                    Creator = x.Creator.Name
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
                    Name = x.Creator.Name
                }
            }).ToList();

            return model;
        }
    }
}
