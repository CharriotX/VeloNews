using Data.Interface.DataModels;

namespace VeloNews.Services.IServices
{
    public interface INewsImageService
    {
        void UploadNewsImages(int newsId, List<IFormFile> images, DateTime creationDate);
    }
}