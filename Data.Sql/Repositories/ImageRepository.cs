using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(WebContext webContext) : base(webContext)
        {
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
    }
}
