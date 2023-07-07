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
            
            return View(model);
        }
    }
}