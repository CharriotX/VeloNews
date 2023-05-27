using Data.Interface.DataModels.AdminDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Repositories
{
    public interface IAdminRepository
    {
        MainAdminPageData GetDataFormAdminMainPage();
    }
}
