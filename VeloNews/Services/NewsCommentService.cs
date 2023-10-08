using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.SignalR;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;
using VeloNews.SignalRHubs;

namespace VeloNews.Services
{
    public class NewsCommentService : INewsCommentService
    {
        private INewsCommentRepository _newsCommentRepository;
        private IAuthenticationService _authenticationService;
        private IUserActivityHubService _activityHubService;
        private IHubContext<AdminUserActivityHub> _hubContext;

        public NewsCommentService(INewsCommentRepository newsCommentRepository,
            IAuthenticationService authenticationService,
            IUserActivityHubService activityHubService,
            IHubContext<AdminUserActivityHub> hubContext)
        {
            _newsCommentRepository = newsCommentRepository;
            _authenticationService = authenticationService;
            _activityHubService = activityHubService;
            _hubContext = hubContext;
        }

        public SaveNewsCommentViewModel SaveComment(int commentId, int newsId, string text)
        {
            if (commentId == 0)
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

                _activityHubService.SaveUserCommentActivityHistory(user.Name, newsId.ToString(), data.Text);

                return model;

            }
            else
            {
                var model = EditComment(commentId, text);
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
