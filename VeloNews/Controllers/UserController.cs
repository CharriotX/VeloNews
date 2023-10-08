using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;
using IAuthenticationService = VeloNews.Services.IServices.IAuthenticationService;

namespace VeloNews.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IAuthenticationService _authenticationService;
        private IUserActivityHubService _userActivityHubService;

        public UserController(IUserService userService,
            IAuthenticationService authenticationService,
            IUserActivityHubService userActivityHubService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _userActivityHubService = userActivityHubService;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var user = _authenticationService.GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = _userService.ShowProfile();

            return View(model);
        }

        [HttpGet]
        public IActionResult EditMyProfile(int userId)
        {
            var model = _userService.GetViewModelForEditProfilePage(userId);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditMyProfile(EditMyProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = _userService.GetViewModelForEditProfilePage(viewModel.UserId);
                return View(model);
            }

            _userService.EditMyProfile(viewModel);

            return RedirectToAction("MyProfile");
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

            var user = _authenticationService.GetUserByNameAndPass(viewModel.Name, viewModel.Password);

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

            _userActivityHubService.UserLogin(user.Id, user.Name);

            return RedirectToAction("MyProfile");
        }

        public async Task<IActionResult> Logout()
        {
            _userActivityHubService.UserLogout();

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _userService.RegistrationUser(viewModel);

            return RedirectToAction("Index", "Home");
        }

    }
}
