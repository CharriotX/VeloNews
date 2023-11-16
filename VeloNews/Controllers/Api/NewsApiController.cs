﻿using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<NewsCardViewModel> GetAllNews()
        {
            var newsCardsViewModels = _newsService.GetAllNewsCards();

            return Ok(newsCardsViewModels);
        }

        [HttpGet("{id}")]
        public ActionResult<ShowNewsViewModel> GetFullNews(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var newsViewModels = _newsService.GetFullNews(id);

            if (newsViewModels == null)
            {
                return NotFound();
            }

            return Ok(newsViewModels);
        }

        [HttpPut]
        public ActionResult EditNews([FromForm] EditNewsViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            _newsService.EditNews(viewModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteNews(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            _newsService.DeleteNews(id);

            return Ok();
        }
    }
}
