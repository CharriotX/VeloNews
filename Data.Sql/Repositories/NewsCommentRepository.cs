using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class NewsCommentRepository : BaseRepository<Comment>, INewsCommentRepository
    {
        public NewsCommentRepository(WebContext webContext) : base(webContext)
        {
        }

        public Comment GetCommentWithUser(int id)
        {
            return _dbSet.Include(x => x.User).SingleOrDefault(x => x.Id == id);
        }
    }
}
