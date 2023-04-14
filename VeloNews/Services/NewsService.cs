using Data.Interface.Models;
using Data.Interface.Repositories;

namespace VeloNews.Services
{
    public class NewsService : INewsService
    {
        private INewsRepository _newsRepository;
        private IImageRepository _imageRepository;

        public NewsService(INewsRepository newsRepository,
            IImageRepository imageRepository)
        {
            _newsRepository = newsRepository;
            _imageRepository = imageRepository;
        }

        public List<News> GetAllNews()
        {
            return _newsRepository.GetAll().ToList();
        }
    }
}
