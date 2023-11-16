using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Repositories;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;

namespace VeloNews.Services
{
    [AutoDiServiceRegistration]
    public class NewsCommentService : INewsCommentService
    {
        private INewsCommentRepository _newsCommentRepository;
        private IAuthenticationService _authenticationService;
        private IUserActivityHubService _activityHubService;

        public NewsCommentService(INewsCommentRepository newsCommentRepository,
            IAuthenticationService authenticationService,
            IUserActivityHubService activityHubService)
        {
            _newsCommentRepository = newsCommentRepository;
            _authenticationService = authenticationService;
            _activityHubService = activityHubService;
        }

        public SaveNewsCommentViewModel SaveComment(SaveNewsCommentApiData dataApi)
        {
            var user = _authenticationService.GetCurrentUser();

            var data = new SaveNewsCommentData
            {
                NewsId = dataApi.NewsId,
                Text = dataApi.Text,
                CreatedTime = DateTime.Now,
                AuthorName = user.Name
            };

            var id = _newsCommentRepository.SaveComment(data);

            var comment = _newsCommentRepository.Get(id);

            var model = new SaveNewsCommentViewModel()
            {
                Id = comment.Id,
                Author = user.Name,
                CreatedTime = comment.CreatedTime.ToString("dd-MM-yyyy, HH:mm"),
                NewsId = dataApi.NewsId,
                AuthorProfileImageUrl = user.UserProfileImageUrl,
                Text = comment.Text
            };

            _activityHubService.SaveUserCommentActivityHistory(user.Name, comment.News.Id.ToString(), comment.Text);

            return model;
        }

        public SaveNewsCommentViewModel EditComment(SaveNewsCommentApiData data)
        {
            var comment = _newsCommentRepository.Get(data.Id);

            comment.Text = data.Text;
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
