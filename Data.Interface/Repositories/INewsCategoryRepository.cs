using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface INewsCategoryRepository : IBaseRepository<NewsCategory>
    {
        NewsCategory GetCategoryByName(string name);
    }
}
