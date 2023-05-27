using Data.Interface.DataModels;
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

        public List<LastComment> GetLastComments()
        {
            var lastComments = _dbSet.Include(x => x.News).Include(x => x.User).OrderByDescending(x => x.Id).Take(10).ToList();
            var data = lastComments.Select(x => new LastComment
            {
                Id = x.Id,
                NewsId = x.News.Id,
                Text = x.Text,
                Creator = new CreatorData
                {
                    Id = x.User.Id,
                    Name = x.User.Name
                }
            }).ToList();

            return data;
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
