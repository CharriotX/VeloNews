using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class AdminService : IAdminService
    {
        private IUserService _userService;
        public AdminService(IUserService userService)
        {
            _userService = userService;
        }

    }
}
