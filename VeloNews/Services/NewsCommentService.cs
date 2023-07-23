using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Repositories;
using VeloNews.Models.NewsViewModels;
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

        public SaveNewsCommentViewModel SaveComment(int newsId, string text)
        {
            var user = _userService.GetCurrentUser();
            var data = new SaveNewsCommentData
            {
                NewsId = new NewsId
                {
                    Id = newsId
                },
                Text = text,
                CreatedTime = DateTime.Now,
                Author = new CreatorData
                {
                    Id = user.Id,
                    Name = user.Name
                }
            };

            var commentId = _newsCommentRepository.SaveComment(data);

            var comment = _newsCommentRepository.Get(commentId);

            var model = new SaveNewsCommentViewModel()
            {
                Author = user.Name,
                CreatedTime = comment.CreatedTime.ToString("dd-MM-yyyy, HH:mm"),
                NewsId = newsId,
                Text = comment.Text
            };

            return model;
        }

        public void RemoveComment(int commentId)
        {
            _newsCommentRepository.Remove(commentId);
        }
    }
}
