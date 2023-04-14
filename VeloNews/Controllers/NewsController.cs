using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models;
using VeloNews.Services;

namespace VeloNews.Controllers
{
    public class NewsController : Controller
    {
        private INewsRepository _newsRepository;
        private IImageRepository _imageRepository;
        private IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;

        public NewsController(INewsRepository newsRepository,
            IImageRepository imageRepository,
            IWebHostEnvironment webHostEnvironment,
            IUserService userService)
        {
            _newsRepository = newsRepository;
            _imageRepository = imageRepository;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var allNews = _newsRepository.GetAll();

            var model = allNews.Select(dbNews => new NewsCardViewModel()
            {
                Id = dbNews.Id,
                Title = dbNews.Title,
                ShortText = dbNews.ShorText,
                CreatedTime = dbNews.CreatedTime,
                Author = dbNews.Author,
                PreviewImage = _imageRepository.GetUrlForPreviewImage(dbNews.Id)
            }).Reverse()
            .ToList();

            return View(model);
        }

        public IActionResult ShowNews(int newsId)
        {
            var dbNews = _newsRepository.Get(newsId);
            var model = new ShowNewsViewModel()
            {
                Title = dbNews.Title,
                ShortText = dbNews.ShorText,
                Text = dbNews.Text,
                CreatedTime = dbNews.CreatedTime,
                Author = dbNews.Author,
                NewsUrlsImages = _imageRepository.GetUrlsForShowNewsImages(newsId)
            };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddNews()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddNews(NewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View(viewModel);
            }

            var author = _userService.GetCurrentUser();

            var dbModel = new News()
            {
                Title = viewModel.Title,
                Text = viewModel.Text,
                ShorText = viewModel.ShortText,
                CreatedTime = DateTime.Now,
                Author = author.Name
            };

            _newsRepository.Save(dbModel);

            var thisNews = _newsRepository.Get(dbModel.Id);

            if (viewModel.NewsImages == null)
            {
                _imageRepository.Save(new Image()
                {
                    Name = "defaultImage",
                    Url = $"/images/defaultNewsPreviewImage.jpg",
                    News = thisNews
                });
            }
            else
            {
                var imageIndex = 1;

                foreach (var file in viewModel.NewsImages)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var folderName = $"post{dbModel.CreatedTime.ToString("ddMMyyyy")}";

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

                    var fileName = $"{thisNews.Id}-{imageIndex}{extention}";

                    var fileNameWithPath = Path.Combine(path, fileName);

                    using (var fs = new FileStream(fileNameWithPath, FileMode.CreateNew))
                    {
                        file.CopyTo(fs);
                    }

                    _imageRepository.Save(new Image()
                    {
                        Name = fileName,
                        Url = $"/images/uploads/news/{folderName}/{fileName}",
                        News = thisNews
                    });
                    imageIndex++;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddComment(int id, string text)
        {
            var news = _newsRepository.Get(id);
            var comment = new CommentViewModel()
            {
                Text = text
            };

            return RedirectToAction();
        }
    }
}
