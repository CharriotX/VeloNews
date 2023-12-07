using Data.Interface.DataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T Get(int id);
        List<T> GetAll();
        void Remove(T model);
        void Remove(int id);
        void Save(T model);
        int Count();
        bool Any();
        PaginatorData<T> GetPaginator(int page, int perPage, string sortField);
    }
}