using Data.Interface.Models.enums;
using System.Globalization;
using VeloNews.Services;
using VeloNews.Services.IServices;

namespace VeloNews.Localization
{
    public class LocalizeMiddleware
    {
        private readonly RequestDelegate _next;
        public LocalizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userService = context.RequestServices
                .GetService(typeof(IAuthenticationService)) as AuthenticationService;

            if (userService.GetCurrentUser() == null)
            {
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-EN");
            }
            else
            {
                switch (userService.GetCurrentUser()?.Language)
                {
                    case UserLanguage.Rus:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
                        break;

                    case UserLanguage.Eng:
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-En");
                        break;
                }
            }

            await _next(context);
        }
    }
}
