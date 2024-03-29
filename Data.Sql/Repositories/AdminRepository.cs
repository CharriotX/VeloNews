﻿using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class AdminRepository : BaseRepository<User>, IAdminRepository
    {
        private IUserRepository _userRepository;
        private INewsRepository _newsRepository;
        private INewsCommentRepository _newsCommentRepository;

        public AdminRepository(WebContext webContext,
            IUserRepository userRepository,
            INewsRepository newsRepository,
            INewsCommentRepository newsCommentRepository) : base(webContext)
        {
            _userRepository = userRepository;
            _newsRepository = newsRepository;
            _newsCommentRepository = newsCommentRepository;
        }

        public List<NewsForAdminPageData> GetAllNewsForAdminPage()
        {
            var allNews = _newsRepository.GetAllNewsWithCreator();

            var data = allNews.Select(x => new NewsForAdminPageData
            {
                Id = x.Id,
                Title = x.Title,
                TimeOfCreation = x.CreatedTime,
                Creator = new NewsCommentAuthorData
                {
                    Id = x.Creator.Id,
                    AuthorName = x.Creator.Name
                }
            }).ToList();

            return data;
        }

        public MainAdminPageData GetDataFormAdminMainPage()
        {
            var usersCount = _userRepository.GetAll().Count();
            var newsCount = _newsRepository.GetAll().Count();
            var newsCommentCount = _newsCommentRepository.GetAll().Count();

            var lastNews = _newsRepository.GetLastNews();
            var lastComments = _newsCommentRepository.GetLastComments();
            var lastRegisterUsers = _userRepository.GetLastRegisteredUsers();

            var data = new MainAdminPageData
            {
                LastPublishedNews = lastNews.Select(x => new LastNewsData
                {
                    Title = x.Title,
                    Id = x.Id,
                    Creator = new NewsCommentAuthorData
                    {
                        Id = x.Creator.Id,
                        AuthorName = x.Creator.AuthorName
                    }
                }).ToList(),
                LastComments = lastComments.Select(x => new LastCommentData
                {
                    Id = x.Id,
                    NewsId = x.NewsId,
                    Text = x.Text,
                    Creator = new NewsCommentAuthorData
                    {
                        Id = x.Creator.Id,
                        AuthorName = x.Creator.AuthorName
                    }
                }).ToList(),
                LastRegisteredUsers = lastRegisterUsers.Select(x => new LastRegisteredUserData
                {
                    Id = x.Id,
                    UserName = x.UserName
                }).ToList(),
                SiteStats = new SiteStatsData
                {
                    UsersCount = usersCount,
                    NewsCommentsCount = newsCommentCount,
                    NewsCount = newsCount
                }
            };

            return data;
        }

        public PaginatorData<News> GetAllNewsPaginator(int page, int perPage, string sortField)
        {
            var data = _newsRepository.GetPaginator(page, perPage, sortField);

            return data;
        }
    }
}
