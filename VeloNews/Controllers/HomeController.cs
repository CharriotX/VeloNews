using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models.ImageViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class HomeController : Controller
    {
        private INewsService _newsService;
        public HomeController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            var model = _newsService.GetNewsForHomePage();
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
    }
}