﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Filters
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;

            if (authService.GetCurrentUserData() == null)
            {
                context.Result = new UnauthorizedResult();
            }

            if (!authService.IsAdmin())
            {
                context.Result = new ForbidResult() ;
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
