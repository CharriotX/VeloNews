using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VeloNews.Models.UserViewModels;
using VeloNews.Services;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private INewsService _newsService;

        public UserController(IUserService userService, INewsService newsService)
        {
            _userService = userService;
            _newsService = newsService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var user = _userService.GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ProfileViewModel()
            {
                Name = user.Name,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.GetUserByNameAndPass(viewModel.Name, viewModel.Password);

            if (user == null)
            {
                return View();
            }

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.AuthenticationMethod, CookieAuthenticationDefaults.AuthenticationScheme)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(UserViewModel viewModel)
        {
            _userService.RegistrationUser(viewModel.Name, viewModel.Password);

            return RedirectToAction("Index", "Home");
        }

    }
}
