using Data.Interface.DataModels.AdminDataModels;
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

        public List<LastCommentData> GetLastComments()
        {
            var lastComments = _dbSet
                .Include(x => x.News)
                .Include(x => x.User)
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToList();

            var data = lastComments.Select(x => new LastCommentData
            {
                Id = x.Id,
                NewsId = x.News.Id,
                Text = x.Text,
                Creator = new NewsCommentAuthorData
                {
                    Id = x.User.Id,
                    AuthorName = x.User.Name
                }
            }).ToList();

            return data;
        }

        public int SaveComment(SaveNewsCommentData data)
        {
            var user = _userRepository.GetUserByUsername(data.AuthorName);
            var news = _newsRepository.Get(data.NewsId);

            var comment = new Comment()
            {
                News = news,
                Text = data.Text,
                CreatedTime = data.CreatedTime,
                User = user
            };

            _dbSet.Add(comment);
            _webContext.SaveChanges();

            return comment.Id;
        }
    }
}
