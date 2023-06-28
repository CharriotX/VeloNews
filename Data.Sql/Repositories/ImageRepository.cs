using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        private INewsRepository _newsRepository;
        public ImageRepository(WebContext webContext, INewsRepository newsRepository) : base(webContext)
        {
            _newsRepository = newsRepository;
        }

        public string GetUrlForPreviewImage(int newsId)
        {
            var news = _dbSet.Where(x => x.News.Id == newsId);
            var url = news.FirstOrDefault().Url;

            return url;
        }

        public List<string> GetUrlsForShowNewsImages(int newsId)
        {
            var news = _dbSet.Where(x => x.News.Id == newsId);
            var urls = news.Select(x => x.Url).ToList();

            return urls;
        }

        public void SaveNewsImages(ImageData data)
        {
            var news = _newsRepository.Get(data.NewsId);
            var model = new Image()
            {
                Name = data.Name,
                Url = data.Url,
                News = news
            };

            _dbSet.Add(model);
            _webContext.SaveChanges();
        }
    }
}
