using Microsoft.AspNetCore.Mvc;
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

        public IActionResult AllNews()
        {
            return View();
        }
    }
}
