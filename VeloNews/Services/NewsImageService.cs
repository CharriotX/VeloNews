using Data.Interface.DataModels;
using Data.Interface.Repositories;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class NewsImageService : INewsImageService
    {
        private INewsImageRepository _newsImageRepository;
        private IWebHostEnvironment _webHostEnvironment;
        public NewsImageService(INewsImageRepository newsImageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _newsImageRepository = newsImageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void UploadNewsImages(int newsId, List<IFormFile> images, DateTime creationDate)
        {
            if (images == null)
            {
                var imageData = new NewsImageData
                {
                    Name = "defaultImage",
                    Url = $"/images/defaultNewsPreviewImage.jpg",
                    NewsId = newsId
                };

                _newsImageRepository.SaveNewsImages(imageData);
            }
            else
            {
                var imageIndex = 1;

                foreach (var file in images)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var folderName = $"post{creationDate.ToString("ddMMyyyy")}";
                    var path = Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        "images",
                        "uploads",
                        "news",
                        folderName
                        );

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fileName = $"{newsId}-{imageIndex}{extention}";

                    var fileNameWithPath = Path.Combine(path, fileName);

                    using (var fs = new FileStream(fileNameWithPath, FileMode.CreateNew))
                    {
                        file.CopyTo(fs);
                    }

                    var imageData = new NewsImageData
                    {
                        Name = fileName,
                        Url = $"/images/uploads/news/{folderName}/{fileName}",
                        NewsId = newsId
                    };

                    _newsImageRepository.SaveNewsImages(imageData);
                    imageIndex++;
                }
            }

        }
    }
}
