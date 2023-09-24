using Data.Interface.DataModels;

namespace VeloNews.Services.IServices
{
    public interface IUserProfileImageService
    {
        void UploadNewProfileImage(IFormFile profileImage, int userId);
    }
}