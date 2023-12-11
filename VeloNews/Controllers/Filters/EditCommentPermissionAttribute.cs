using Data.Interface.DataModels.NewsDataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Filters
{
    public class EditCommentPermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;
            var commentService = context.HttpContext.RequestServices.GetService(typeof(INewsCommentService)) as INewsCommentService;

            var data = context.ActionArguments["commentData"] as SaveNewsCommentApiData;

            if (context.ActionArguments.ContainsKey("commentData"))
            {
                if (authService.GetCurrentUserData() == null)
                {
                    context.Result = new UnauthorizedResult();
                }

                if (authService.IsAdmin() || authService.IsNewsModerator() || !commentService.UserIsAuthor(data.Id))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
