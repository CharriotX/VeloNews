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

        public AdminController(IUserService userService, IAdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            var viewModel = _adminService.GetAdminMainPageViewModel();
            return View(viewModel);
        }

        public IActionResult AllNews(int page = 1, int perPage = 8)
        {
            var model = _adminService.GetAllNewsForPagginator(page, perPage);
            return View(model);
        }
    }
}
