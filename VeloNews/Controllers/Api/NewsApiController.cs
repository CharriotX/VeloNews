using Data.Interface.DataModels.NewsDataModels;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Api
{
    [ApiController]
    [Route("/api/news")]
    public class NewsApiController: Controller
    {
        private INewsCommentService _newsCommentService;

        public NewsApiController(INewsCommentService newsCommentService)
        {
            _newsCommentService = newsCommentService;
        }

        [Route("AddComment")]
        public JsonResult AddComment(int newsId, string text)
        {
            var model = _newsCommentService.SaveComment(newsId, text);
           
            return Json(model);
        }

        [Route("RemoveComment")]
        public void RemoveComment(int commentId)
        {
            _newsCommentService.RemoveComment(commentId);
        }
    }
}
