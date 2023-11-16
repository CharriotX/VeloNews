using Data.Interface.DataModels.NewsDataModels;
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

        [HttpPost]
        public ActionResult AddComment([FromForm] SaveNewsCommentApiData commentData)
        {
            var model = _newsCommentService.SaveComment(commentData);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveComment(int id)
        {
            _newsCommentService.RemoveComment(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult EditComment([FromForm] SaveNewsCommentApiData commentData)
        {
            var model = _newsCommentService.EditComment(commentData);

            return Ok(model);
        }

    }
}
