using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class NewsController : Controller
    {
        private INewsService _newsService;
        private INewsCommentService _newsCommentService;
        private INewsRepository _newsRepository;
        private INewsCategoryRepository _newsCategoryRepository;
        private IImageRepository _imageRepository;
        private IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;

        public NewsController(INewsRepository newsRepository,
            IImageRepository imageRepository,
            IWebHostEnvironment webHostEnvironment,
            IUserService userService,
            INewsService newsService,
            INewsCommentService newsCommentService,
            INewsCategoryRepository newsCategoryRepository)
        {
            _newsRepository = newsRepository;
            _imageRepository = imageRepository;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _newsService = newsService;
            _newsCommentService = newsCommentService;
            _newsCategoryRepository = newsCategoryRepository;
        }

        public IActionResult Index()
        {
            var models = _newsService.GetAllNewsCards();

            return View(models);
        }

        public IActionResult ShowNews(int newsId)
        {
            var model = _newsService.GetFullNews(newsId);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddNews(int newsId)
        {
            var modelWithCategoryList = _newsService.GetAllNewsCategories();
            return View(modelWithCategoryList);
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
            var category = _newsCategoryRepository.Get(viewModel.SelectedCategoryId);

            var dbModel = new News()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Text = viewModel.Text,
                ShorText = viewModel.ShortText,
                CreatedTime = DateTime.Now,
                Creator = author,
                Category = category
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

        [HttpGet]
        public IActionResult EditNews(int newsId)
        {
            var model = _newsService.GetNewsForEdit(newsId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditNews(EditNewsViewModel viewModel)
        {
            _newsService.EditNews(viewModel.Id,
                viewModel.Title,
                viewModel.Text,
                viewModel.ShortText);

            return RedirectToAction("ShowNews", "News", new {newsId = viewModel.Id});
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(SaveNewsCommentViewModel viewModel)
        {
            _newsCommentService.SaveComment(viewModel);
            return RedirectToAction("ShowNews", new { @newsId = viewModel.NewsId });
        }
    }
}
