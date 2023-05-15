using Microsoft.AspNetCore.Mvc;

namespace VeloNews.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllNews()
        {
            return View();
        }
    }
}
