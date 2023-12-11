using Microsoft.AspNetCore.Mvc;
using VeloNews.Controllers.Filters;
using VeloNews.Models.enums;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    [IsAdmin]
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

        public IActionResult AllNews(string sortField = "Id", int page = 1, int perPage = 8)
        {
            ViewData["CurrentSort"] = sortField;

            var model = _newsService.GetAllNewsForAdminPagginator(page, perPage, sortField);
            return View(model);
        }
        public IActionResult AllUsers(string sortField = "Id", int page = 1, int perPage = 20)
        {
            ViewData["CurrentSort"] = sortField;

            var model = _userService.UsersForAdminPage(page, perPage, sortField);
            return View(model);
        }

        public IActionResult UserActivity()
        {
            var model = _userActivityHubService.GetAllActions();
            return View(model);
        }
    }
}
