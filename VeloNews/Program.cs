using Data.Interface.Repositories;
using Data.Sql;
using Data.Sql.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
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

builder.Services.AddScoped<IPaginatorService, PaginatorService>();

builder.Services.AddScoped<IAdminService>(x =>
    new AdminService(
        x.GetService<IUserService>(),
        x.GetService<IAdminRepository>()));

builder.Services.AddScoped<IAdminRepository>(x =>
    new AdminRepository(
        x.GetService<IUserRepository>(),
        x.GetService<INewsRepository>(),
        x.GetService<INewsCommentRepository>()
        ));

builder.Services.AddScoped<INewsCommentService>(x =>
    new NewsCommentService(
        x.GetService<INewsCommentRepository>(),
        x.GetService<IAuthenticationService>(),
        x.GetService<IUserActivityHubService>(),
        x.GetService<IHubContext<AdminUserActivityHub>>()));

builder.Services.AddScoped<IUserService>(x =>
    new UserService(
        x.GetService<IUserRepository>(),
        x.GetService<IHttpContextAccessor>(),
        x.GetService<IUserProfileImageRepository>(),
        x.GetService<IUserProfileImageService>(),
        x.GetService<IAuthenticationService>()
        ));

builder.Services.AddScoped<INewsService>(x =>
    new NewsService(
        x.GetService<INewsRepository>(),
        x.GetService<INewsImageRepository>(),
        x.GetService<INewsCategoryRepository>(),
        x.GetService<IPaginatorService>(),
        x.GetService<IAuthenticationService>(),
        x.GetService<INewsImageService>()
        ));

builder.Services.AddScoped<IUserProfileImageService>(x =>
    new UserProfileImageService(
        x.GetService<IUserRepository>(),
        x.GetService<IWebHostEnvironment>(),
        x.GetService<IUserProfileImageRepository>()
        ));

builder.Services.AddScoped<IAuthenticationService>(x =>
    new AuthenticationService(
        x.GetService<IUserRepository>(),
        x.GetService<IHttpContextAccessor>()
        ));


builder.Services.AddScoped<IUserActivityHubService>(x =>
    new UserActivityHubService(
        x.GetService<IHubContext<AdminUserActivityHub>>(),
        x.GetService<IUserActivityRepository>(),
        x.GetService<IAuthenticationService>()));

builder.Services.AddScoped<INewsImageService>(x =>
    new NewsImageService(
        x.GetService<INewsImageRepository>(),
        x.GetService<IWebHostEnvironment>()));

builder.Services.AddScoped<IUserProfileImageRepository>(x =>
    new UserProfileImageRepository(
        x.GetService<WebContext>(),
        x.GetService<IUserRepository>()));

builder.Services.AddScoped<INewsRepository>(x =>
    new NewsRepository(
        x.GetService<WebContext>(),
        x.GetService<INewsCategoryRepository>(),
        x.GetService<IUserRepository>()
        ));

builder.Services.AddScoped<IUserActivityRepository>(x =>
    new UserActivityRepository(x.GetService<WebContext>()));

builder.Services.AddScoped<INewsCategoryRepository>(x =>
    new NewsCategoryRepository(x.GetService<WebContext>()));

builder.Services.AddScoped<INewsCommentRepository>(x =>
    new NewsCommentRepository(
        x.GetService<WebContext>(),
        x.GetService<INewsRepository>(),
        x.GetService<IUserRepository>()));

builder.Services.AddScoped<INewsImageRepository>(x =>
    new NewsImageRepository(
        x.GetService<WebContext>(),
        x.GetService<INewsRepository>()
        ));

builder.Services.AddScoped<IUserRepository>(x =>
    new UserRepository(
        x.GetService<WebContext>()));


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

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
app.UseStaticFiles();

app.UseRouting();

//Who i am
app.UseAuthentication();

//Where could i go
app.UseAuthorization();

app.MapHub<AdminUserActivityHub>("/userActivity");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
