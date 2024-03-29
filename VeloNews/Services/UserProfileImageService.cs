﻿using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Repositories;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;

namespace VeloNews.Services
{
    [AutoDiServiceRegistration]
    public class UserProfileImageService : IUserProfileImageService
    {
        private IUserRepository _userRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IUserProfileImageRepository _userProfileImageRepository;
        public UserProfileImageService(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, IUserProfileImageRepository userProfileImageRepository)
        {
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _userProfileImageRepository = userProfileImageRepository;
        }

        public void UploadNewProfileImage(IFormFile profileImage, int userId)
        {
            var user = _userRepository.GetUserById(userId);
            var newImage = profileImage;

            var extention = Path.GetExtension(newImage.FileName);
            var folderNameWord = $"{user.Name.Substring(0, 1).ToString().ToUpper()}";
            var folderName = $"{user.Name.ToUpper()}";
            var path = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "userProfileImages",
                folderNameWord,
                folderName
                );

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = $"{user.Name}{extention}";
            var fileNameWithPath = Path.Combine(path, fileName);

            if (fileNameWithPath.Any())
            {
                File.Delete(fileNameWithPath);
            }

            using (var fs = new FileStream(fileNameWithPath, FileMode.CreateNew))
            {
                newImage.CopyTo(fs);
            }

            var imageData = new MyProfileImageData
            {
                Name = fileName,
                Url = $"/images/userProfileImages/{folderNameWord}/{folderName}/{fileName}",
                UserId = userId
            };

            _userProfileImageRepository.EditProfileImage(imageData);
        }
    }
}
