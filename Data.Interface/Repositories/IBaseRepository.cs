using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T Get(int id);
        List<T> GetAll();
        void Remove(T model);
        void Save(T model);
    }
}