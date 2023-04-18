using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(WebContext webContext) : base(webContext)
        {
        }

        public News GetNewsWithComments(int newsId)
        {
            return _dbSet
                .Include(x => x.NewsComments)
                .FirstOrDefault(x => x.Id == newsId);
        }
    }
}
