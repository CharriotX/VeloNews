using Data.Interface.DataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T Get(int id);
        List<T> GetAll();
        void Remove(T model);
        void Save(T model);
        bool Any();
        PaginatorData<T> GetPaginator(int page, int perPage);
    }
}