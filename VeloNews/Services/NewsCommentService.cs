using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Repositories;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class NewsCommentService : INewsCommentService
    {
        private INewsService _newsService;
        private IUserService _userService;
        private INewsCommentRepository _newsCommentRepository;

        public NewsCommentService(INewsService newsService, IUserService userService, INewsCommentRepository newsCommentRepository)
        {
            _newsService = newsService;
            _userService = userService;
            _newsCommentRepository = newsCommentRepository;
        }

        public void SaveComment(int newsId, string text)
        {
            var user = _userService.GetCurrentUser();
            var data = new SaveNewsCommentData
            {
                NewsId = new NewsId
                {
                    Id = newsId
                },
                Text = text,
                Author = new Creator
                {
                    Id = user.Id
                }
            };

            _newsCommentRepository.SaveComment(data);
        }
    }
}
