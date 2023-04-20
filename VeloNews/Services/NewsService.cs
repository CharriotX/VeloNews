using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models;
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

        public ShowNewsViewModel GetFullNews(int newsId)
        {
            var news = _newsRepository.GetNewsWithCommentsandImages(newsId);

            var model = new ShowNewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                ShortText = news.ShortText,
                CreatedTime = news.CreatedTime,
                Author = news.Author,
                NewsUrlsImages = news.NewsUrlsImages.Select(x=> new NewsImageViewModel()
                {
                    Url = x.Url
                }).ToList(),
                NewsComments = news.NewsComments.Select( x => new NewsCommentViewModel()
                {
                    Author = x.Author.Name,
                    Text = x.Text
                }).ToList()
            };

            return model;
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
