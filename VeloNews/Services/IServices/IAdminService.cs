using VeloNews.Models.AdminViewModels;

namespace VeloNews.Services.IServices
{
    public interface IAdminService
    {
        AdminMainPageViewModel GetAdminMainPageViewModel();
    }
}