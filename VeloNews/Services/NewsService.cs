using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models.NewsViewModels;
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

        public List<NewsCardViewModel> GetAllNewsCards()
        {
            var allNews = _newsRepository.GetAllNewsCards();

            var models = allNews.Select(model => new NewsCardViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ShortText = model.ShortText,
                CreatedTime = model.CreatedTime,
                Author = model.Author,
                PreviewImage = model.PreviewImage
            }).Reverse().ToList();

            return models;
        }

        public ShowNewsViewModel GetFullNews(int newsId)
        {
            var news = _newsRepository.GetNewsWithCommentsAndImages(newsId);

            var model = new ShowNewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                ShortText = news.ShortText,
                CreatedTime = news.CreatedTime,
                Author = news.Author,
                NewsUrlsImages = news.NewsUrlsImages.Select(x => new NewsImageViewModel()
                {
                    Url = x.Url
                }).ToList(),
                NewsComments = news.NewsComments.Select(x => new NewsCommentViewModel()
                {
                    Author = x.Author.Name,
                    Text = x.Text
                }).Reverse().ToList()
            };

            return model;
        }
    }
}
