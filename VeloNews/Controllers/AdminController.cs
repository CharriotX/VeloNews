using Microsoft.AspNetCore.Mvc;
using VeloNews.Models;
using VeloNews.Models.AdminViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        private IAdminService _adminService;
        private INewsService _newsService;

        public AdminController(IUserService userService, IAdminService adminService, INewsService newsService)
        {
            _userService = userService;
            _adminService = adminService;
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            var viewModel = _adminService.GetAdminMainPageViewModel();
            return View(viewModel);
        }

        public IActionResult AllNews(int page = 1, int perPage = 8)
        {
            var model = _newsService.GetAllNewsForPagginator(page, perPage);
            return View(model);
        }
    }
}
