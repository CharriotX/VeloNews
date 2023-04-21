using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models.ImageViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class HomeController : Controller
    {
        private IImageRepository _imageRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private INewsService _newsService;

        public HomeController(IImageRepository imageRepository,
            IWebHostEnvironment webHostEnvironment,
            INewsService newsService)
        {
            _imageRepository = imageRepository;
            _webHostEnvironment = webHostEnvironment;
            _newsService = newsService;
        }

        public IActionResult BestNews(int newsId)
        {
            return View();
        }

        public IActionResult Index()
        {
            var images = _imageRepository.GetAll();
            var model = images.Select(x => new ImageViewModel()
            {
                Name = x.Name,
                Url = x.Url
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(ImageViewModel viewModel)
        {
            var image = new Image()
            {
                Name = viewModel.Name,
                Url = ""
            };

            _imageRepository.Save(image);


            var extention = Path.GetExtension(viewModel.UploadImage.FileName);
            var fileName = $"{image.Id}{extention}";
            var path = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "uploads",
                "pictures",
                fileName);

            using (var fs = new FileStream(path, FileMode.CreateNew))
            {
                viewModel.UploadImage.CopyTo(fs);
            }

            image.Url = $"/images/uploads/pictures/{fileName}";
            _imageRepository.Save(image);
            
            return RedirectToAction("Index");
        }
    }
}