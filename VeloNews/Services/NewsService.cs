using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class NewsService : INewsService
    {
        private IUserService _userService;
        private INewsRepository _newsRepository;
        private IImageRepository _imageRepository;
        private INewsCommentRepository _newsCommentRepository;

        public NewsService(INewsRepository newsRepository,
            IImageRepository imageRepository,
            INewsCommentRepository newsCommentRepository,
            IUserService userService)
        {
            _newsRepository = newsRepository;
            _imageRepository = imageRepository;
            _newsCommentRepository = newsCommentRepository;
            _userService = userService;
        }

        public List<News> GetAllNews()
        {
            return _newsRepository.GetAll().ToList();
        }

        public News GetNewsWithComments(int id)
        {
            return _newsRepository.GetNewsWithComments(id);
        }

        public void SaveComment(int newsId, string text)
        {
            var news = GetNewsWithComments(newsId);
            var comment = new Comment
            {
                News = news,
                Text = text,
                User = _userService.GetCurrentUser()
            };

            _newsCommentRepository.Save(comment);
        }
    }
}
