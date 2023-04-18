using Data.Interface.Repositories;
using Data.Sql;
using Data.Sql.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using VeloNews.Services;
using VeloNews.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/User/Login";
        option.AccessDeniedPath = "/User/AccessDenied";
        option.Cookie.Name = "VeloNewsCookie";
    });


builder.Services.AddScoped<IUserService>(x =>
    new UserService(
        x.GetService<IUserRepository>(),
        x.GetService<IHttpContextAccessor>()));

builder.Services.AddScoped<INewsService>(x =>
    new NewsService(
        x.GetService<INewsRepository>(),
        x.GetService<IImageRepository>(),
        x.GetService<INewsCommentRepository>(),
        x.GetService<IUserService>()
        ));


var dataSqlStartup = new Startup();
dataSqlStartup.RegisterDbContext(builder.Services);


builder.Services.AddScoped<INewsRepository>(x =>
    new NewsRepository(x.GetService<WebContext>()));
builder.Services.AddScoped<INewsCommentRepository>(x =>
    new NewsCommentRepository(x.GetService<WebContext>()));
builder.Services.AddScoped<IImageRepository>(x =>
    new ImageRepository(x.GetService<WebContext>()));
builder.Services.AddScoped<IUserRepository>(x =>
    new UserRepository(x.GetService<WebContext>()));


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
