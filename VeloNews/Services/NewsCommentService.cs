using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Repositories;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class NewsCommentService : INewsCommentService
    {
        private INewsCommentRepository _newsCommentRepository;
        private IAuthenticationService _authenticationService;

        public NewsCommentService(INewsCommentRepository newsCommentRepository, 
            IAuthenticationService authenticationService)
        {
            _newsCommentRepository = newsCommentRepository;
            _authenticationService = authenticationService;
        }

        public SaveNewsCommentViewModel SaveComment(int newsId, string text)
        {
            var user = _authenticationService.GetCurrentUser();
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
