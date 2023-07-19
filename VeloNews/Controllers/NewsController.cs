using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers
{
    public class NewsController : Controller
    {
        private INewsService _newsService;
        private INewsCommentService _newsCommentService;

        public NewsController(INewsService newsService,
            INewsCommentService newsCommentService)
        {
            _newsService = newsService;
            _newsCommentService = newsCommentService;
        }

        public IActionResult Index(int page = 1, int perPage = 6)
        {
            var model = _newsService.GetNewsCardForPaginator(page, perPage);

            return View(model);
        }

        public IActionResult ShowNews(int newsId)
        {
            var model = _newsService.GetFullNews(newsId);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddNews()
        {
            var modelWithCategoryList = _newsService.GetAllNewsCategories();
            return View(modelWithCategoryList);
        }

        [Authorize]
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

        [HttpGet]
        public IActionResult EditNews(int newsId)
        {
            var model = _newsService.GetNewsForEdit(newsId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditNews(EditNewsViewModel viewModel)
        {
            _newsService.EditNews(
                viewModel.Id,
                viewModel.Title,
                viewModel.Text,
                viewModel.ShortText);

            return RedirectToAction("ShowNews", "News", new { newsId = viewModel.Id });
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(SaveNewsCommentViewModel viewModel)
        {
            _newsCommentService.SaveComment(viewModel);
            return RedirectToAction("ShowNews", new { @newsId = viewModel.NewsId });
        }
    }
}
