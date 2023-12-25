using Data.Sql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VeloNews.Localization;
using VeloNews.Services.Helpers;
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


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/Info/INFO-LOG-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day)
    .WriteTo.File("Logs/Error/ERROR-LOG-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<WebContext>();
    context.Database.Migrate();
}

app.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSerilogRequestLogging();

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
