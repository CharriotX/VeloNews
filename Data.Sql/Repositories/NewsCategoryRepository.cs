using Data.Interface.Models;
using Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Sql.Repositories
{
    public class NewsCategoryRepository : BaseRepository<NewsCategory>, INewsCategoryRepository
    {
        public NewsCategoryRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
