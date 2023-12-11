using Microsoft.AspNetCore.Mvc;
using VeloNews.Controllers.Filters;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class NewsController : Controller
    {
        private INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index(string categoryName, int page = 1)
        {
            ViewData["CategoryName"] = categoryName;
            var model = _newsService.GetNewsByCategoryWithPagination(categoryName, page);

            return View(model);
        }

        public IActionResult ShowNews(int newsId)
        {
            var model = _newsService.GetFullNews(newsId);
            return View(model);
        }

        [IsNewsModerator]
        [HttpGet]
        public IActionResult AddNews()
        {
            var modelWithCategoryList = _newsService.GetAllNewsCategories();
            return View(modelWithCategoryList);
        }

        [IsNewsModerator]
        [HttpPost]
        public IActionResult AddNews(AddNewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View(viewModel);
            }

            _newsService.SaveNews(viewModel);

            return RedirectToAction("Index");
        }

        [IsNewsModerator]
        [HttpGet]
        public IActionResult EditNews(int newsId)
        {
            var model = _newsService.GetNewsForEdit(newsId);
            return View(model);
        }

        [IsNewsModerator]
        [HttpPost]
        public IActionResult EditNews(EditNewsViewModel viewModel)
        {
            _newsService.EditNews(viewModel);

            return RedirectToAction("ShowNews", "News", new { newsId = viewModel.Id });
        }

        [IsNewsModerator]
        public IActionResult RemoveNews(int newsId)
        {
            if (newsId == 0)
            {
                return BadRequest();
            }

            _newsService.DeleteNews(newsId);

            return RedirectToAction("AllNews", "Admin");
        }
    }
}
