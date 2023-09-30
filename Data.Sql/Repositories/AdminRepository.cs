using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Sql.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private IUserRepository _userRepository;
        private INewsRepository _newsRepository;
        private INewsCommentRepository _newsCommentRepository;

        public AdminRepository(IUserRepository userRepository,
            INewsRepository newsRepository,
            INewsCommentRepository newsCommentRepository)
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
                Creator = new CommentAuthorData
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
                LastPublishedNews = lastNews.Select(x => new LastNews
                {
                    Title = x.Title,
                    Id = x.Id,
                    Creator = new CommentAuthorData
                    {
                        Id = x.Creator.Id,
                        AuthorName = x.Creator.AuthorName
                    }
                }).ToList(),
                LastComments = lastComments.Select(x => new LastComment
                {
                    Id = x.Id,
                    NewsId = x.NewsId,
                    Text = x.Text,
                    Creator = new CommentAuthorData
                    {
                        Id = x.Creator.Id,
                        AuthorName = x.Creator.AuthorName
                    }
                }).ToList(),
                LastRegisteredUsers = lastRegisterUsers.Select(x => new LastRegisteredUser
                {
                    Id = x.Id,
                    UserName = x.UserName
                }).ToList(),
                SiteStats = new SiteStats
                {
                    UsersCount = usersCount,
                    NewsCommentsCount = newsCommentCount,
                    NewsCount = newsCount
                }
            };

            return data;
        }

        public PaginatorData<News> GetAllNewsPaginator(int page, int perPage)
        {
            var data = _newsRepository.GetPaginator(page, perPage);

            return data;
        }
    }
}
