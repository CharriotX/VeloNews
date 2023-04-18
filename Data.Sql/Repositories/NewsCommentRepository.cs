using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class NewsCommentRepository : BaseRepository<Comment>, INewsCommentRepository
    {
        public NewsCommentRepository(WebContext webContext) : base(webContext)
        {
        }
    }
}
