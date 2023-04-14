using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        string GetUrlForPreviewImage(int newsId);
        List<string> GetUrlsForShowNewsImages(int newsId);
    }
}