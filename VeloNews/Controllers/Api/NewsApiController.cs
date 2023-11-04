using Data.Interface.DataModels.NewsDataModels;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Api
{
    [ApiController]
    [Route("/api/news")]
    public class NewsApiController : Controller
    {
        private INewsService _newsService;

        public NewsApiController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public List<NewsCardViewModel> GetAll()
        {
            var newsCardsViewModels = _newsService.GetAllNewsCards();

            return newsCardsViewModels; 
        }

        [HttpGet("{id}")]
        public ShowNewsViewModel GetFullNews(int id)
        {
            var newsViewModels = _newsService.GetFullNews(id);

            return newsViewModels;
        }

        [HttpPost]
        public ActionResult<AddNewsViewModel> Create(AddNewsViewModel viewModel)
        {
            var newsViewModel = _newsService.SaveNews(viewModel);

            return Ok(newsViewModel);
        }
    }
}
