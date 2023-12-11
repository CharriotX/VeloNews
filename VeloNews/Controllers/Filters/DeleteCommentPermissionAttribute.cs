using Data.Interface.DataModels.NewsDataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Filters
{
    public class DeleteCommentPermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;
            var commentService = context.HttpContext.RequestServices.GetService(typeof(INewsCommentService)) as INewsCommentService;

            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = context.ActionArguments["id"] as int?;

                if (authService.GetCurrentUserData() == null)
                {
                    context.Result = new UnauthorizedResult();
                }

                if (authService.IsAdmin() || authService.IsNewsModerator() || !commentService.UserIsAuthor((int)id))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
