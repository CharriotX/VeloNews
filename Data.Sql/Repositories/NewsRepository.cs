using Data.Interface.DataModels.NewsDataModels;
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

        public List<NewsCardsData> GetAllNewsCards()
        {
            return _dbSet
                .Include(x => x.NewsImages)
                .Include(x => x.Creator)
                .Select(dbNews => new NewsCardsData
                {
                    Id = dbNews.Id,
                    Title = dbNews.Title,
                    ShortText = dbNews.ShorText,
                    CreatedTime = dbNews.CreatedTime,
                    Author = dbNews.Creator.Name,
                    PreviewImage = dbNews.NewsImages.FirstOrDefault().Url
                }).ToList();
        }
        public News GetNewsWithComments(int newsId)
        {
            return _dbSet
                 .Include(x => x.NewsImages)
                 .Include(x => x.NewsComments)
                 .ThenInclude(u => u.User)
                 .FirstOrDefault(x => x.Id == newsId);
        }
        public NewsWithCommentsAndImagesData GetNewsWithCommentsAndImages(int newsId)
        {
            var dbNews = _dbSet
                .Include(x => x.NewsImages)
                .Include(x => x.Creator)
                .Include(x => x.NewsComments)
                .ThenInclude(x => x.User)
                .SingleOrDefault(x => x.Id == newsId);

            var data = new NewsWithCommentsAndImagesData
            {
                Id = dbNews.Id,
                Text = dbNews.Text,
                Title = dbNews.Title,
                ShortText = dbNews.ShorText,
                CreatedTime = dbNews.CreatedTime,
                Author = dbNews.Creator.Name,
                NewsUrlsImages = dbNews
                    .NewsImages
                    .Select(x => new ImageUrlsForShowNews
                    {
                        Url = x.Url
                    }).ToList(),
                NewsComments = dbNews
                    .NewsComments
                    .Select(x => new CommentsNews
                    {
                        Text = x.Text,
                        Author = new CommentAuthor
                        {
                            Name = x.User.Name
                        }
                    }).ToList()
            };

            return data;
        }
    }
}
