using VeloNews.Models;
using VeloNews.Models.AdminViewModels;

namespace VeloNews.Services.IServices
{
    public interface IAdminService
    {
        AdminMainPageViewModel GetAdminMainPageViewModel();
        List<NewsForAdminPageViewModel> GetNewsListForAdminPageViewModel();
        PaginatorViewModel<NewsForAdminPageViewModel> GetAllNewsForPagginator(int page, int perPage);
    }
}