using Data.Interface.DataModels;
using Data.Interface.Repositories;
using Data.Sql.Repositories;
using Data.Sql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using VeloNews.Models;
using VeloNews.Models.AdminViewModels;
using VeloNews.Models.NewsViewModels;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class ImageService : IImageService
    {
        private IUserService _userService;
        private IAdminRepository _adminRepository;
        public ImageService(IUserService userService, IAdminRepository adminRepository)
        {
            _userService = userService;
            _adminRepository = adminRepository;
        }



    }
}
