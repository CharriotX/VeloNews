using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface INewsImageRepository : IBaseRepository<NewsImage>
    {
        string GetUrlForPreviewImage(int newsId);
        List<string> GetUrlsForShowNewsImages(int newsId);
        void SaveNewsImages(NewsImageData data);
    }
}