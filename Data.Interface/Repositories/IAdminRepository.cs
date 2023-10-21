using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Repositories
{
    public interface IAdminRepository : IBaseRepository<User>
    {
        MainAdminPageData GetDataFormAdminMainPage();
        List<NewsForAdminPageData> GetAllNewsForAdminPage();
        PaginatorData<News> GetAllNewsPaginator(int page, int perPage);
    }
}
