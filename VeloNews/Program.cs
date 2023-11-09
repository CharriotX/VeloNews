using Data.Interface.Repositories;
using Data.Sql;
using Data.Sql.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using VeloNews.Localization;
using VeloNews.Services;
using VeloNews.Services.Helpers;
using VeloNews.Services.IServices;
using VeloNews.SignalRHubs;
using VeloNews.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/User/Login";
        option.AccessDeniedPath = "/User/AccessDenied";
        option.Cookie.Name = "VeloNewsCookie";
    });

var dataSqlStartup = new Startup();
dataSqlStartup.RegisterDbContext(builder.Services);

var diRepositoryRegisterHelper = new DiRegistrationHelper();
diRepositoryRegisterHelper.RegisterAllRepositories(builder.Services);
diRepositoryRegisterHelper.RegisterAllServices(builder.Services);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
                      {
                          builder.WithOrigins("*");
                      });
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});




var app = builder.Build();

app.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseCors();

app.UseStaticFiles();

app.UseRouting();

//Who i am
app.UseAuthentication();

//Where could i go
app.UseAuthorization();

app.UseMiddleware<LocalizeMiddleware>();

app.MapHub<AdminUserActivityHub>("/userActivity");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
