using Microsoft.AspNetCore.Mvc;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Api
{
    [ApiController]
    [Route("/api/newsComments")]
    public class NewsCommentApiController : Controller
    {
        private INewsCommentService _newsCommentService;

        public NewsCommentApiController(INewsCommentService newsCommentService)
        {
            _newsCommentService = newsCommentService;
        }

        [Route("AddComment")]
        public JsonResult AddComment(int commentId, int newsId, string text)
        {
            var model = _newsCommentService.SaveComment(commentId, newsId, text);

            return  Json(model);
        }

        [Route("RemoveComment")]
        public void RemoveComment(int commentId)
        {
            _newsCommentService.RemoveComment(commentId);
        }
    }
}
