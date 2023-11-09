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
        private IUserActivityHubService _userActivityHubService;

        public AdminController(IUserService userService, IAdminService adminService, INewsService newsService, IUserActivityHubService userActivityHubService)
        {
            _userService = userService;
            _adminService = adminService;
            _newsService = newsService;
            _userActivityHubService = userActivityHubService;
        }

        public IActionResult Index()
        {
            var viewModel = _adminService.GetAdminMainPageViewModel();
            return View(viewModel);
        }

        public IActionResult AllNews(int page = 1, int perPage = 8)
        {
            var model = _newsService.GetAllNewsForAdminPagginator(page, perPage);
            return View(model);
        }
        public IActionResult UserActivity()
        {
            var model = _userActivityHubService.GetAllActions();
            return View(model);
        }
    }
}
