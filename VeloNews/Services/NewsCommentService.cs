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

        public void SaveComment(SaveNewsCommentViewModel viewModel)
        {
            var user = _userService.GetCurrentUser();
            var data = new SaveNewsCommentData
            {
                NewsId = new NewsId
                {
                    Id = viewModel.NewsId
                },
                Text = viewModel.Text,
                CreatedTime = DateTime.Now,
                Author = new Creator
                {
                    Id = user.Id
                }
            };

            _newsCommentRepository.SaveComment(data);
        }
    }
}
