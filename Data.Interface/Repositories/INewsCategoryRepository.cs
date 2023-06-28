using Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Repositories
{
    public interface INewsCategoryRepository : IBaseRepository<NewsCategory>
    {
        NewsCategory GetCategoryByName(string name);
    }
}
