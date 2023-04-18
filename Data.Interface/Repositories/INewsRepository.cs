using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface INewsRepository : IBaseRepository<News>
    {
        News GetNewsWithComments(int newsId);
    }
}
