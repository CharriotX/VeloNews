using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class NewsCommentRepository : BaseRepository<Comment>, INewsCommentRepository
    {
        private INewsRepository _newsRepository;
        private IUserRepository _userRepository;
        public NewsCommentRepository(WebContext webContext,
            INewsRepository newsRepository,
            IUserRepository userRepository) : base(webContext)
        {
            _newsRepository = newsRepository;
            _userRepository = userRepository;
        }

        public void SaveComment(SaveNewsCommentData data)
        {
            var comment = new Comment()
            {
                News = _newsRepository.Get(data.NewsId.Id),
                Text = data.Text,
                CreatedTime = data.CreatedTime,
                User = _userRepository.Get(data.Author.Id)
            };

            _dbSet.Add(comment);
            _webContext.SaveChanges();
        }
    }
}
