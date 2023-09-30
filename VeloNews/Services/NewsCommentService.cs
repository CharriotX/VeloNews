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

        public SaveNewsCommentViewModel SaveComment(int commentId, int newsId, string text)
        {
            if(commentId == 0)
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
                    Author = new CommentAuthorData
                    {
                        Id = user.Id,
                        AuthorName = user.Name,
                        AuthorProfileImageUrl = user.UserProfileImage.Url
                    }
                };

                var id = _newsCommentRepository.SaveComment(data);

                var comment = _newsCommentRepository.Get(id);

                var model = new SaveNewsCommentViewModel()
                {
                    Author = user.Name,
                    CreatedTime = comment.CreatedTime.ToString("dd-MM-yyyy, HH:mm"),
                    NewsId = newsId,
                    AuthorProfileImageUrl = user.UserProfileImage.Url,
                    Text = comment.Text
                };

                return model;
            }
            else
            {
                var model = EditComment(commentId,text);
                return model;
            }      

        }

        public SaveNewsCommentViewModel EditComment(int commentId, string text)
        {
            var comment = _newsCommentRepository.Get(commentId);

            comment.Text = text;
            comment.CreatedTime = DateTime.Now;

            _newsCommentRepository.Save(comment);

            return new SaveNewsCommentViewModel()
            {
                Text = comment.Text,
                CreatedTime = comment.CreatedTime.ToString("dd-MM-yyyy, HH:mm")
            };
        }

        public void RemoveComment(int commentId)
        {
            _newsCommentRepository.Remove(commentId);
        }
    }
}
