using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class NewsCategoryRepository : BaseRepository<NewsCategory>, INewsCategoryRepository
    {
        public NewsCategoryRepository(WebContext webContext) : base(webContext)
        {
        }

        public NewsCategory GetCategoryByName(string name)
        {
            return _dbSet.SingleOrDefault(x => x.Name == name);
        }
    }
}
