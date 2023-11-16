using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IAdminRepository : IBaseRepository<User>
    {
        MainAdminPageData GetDataFormAdminMainPage();
        List<NewsForAdminPageData> GetAllNewsForAdminPage();
        PaginatorData<News> GetAllNewsPaginator(int page, int perPage);
    }
}
