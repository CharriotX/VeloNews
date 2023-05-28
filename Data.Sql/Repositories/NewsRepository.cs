using Data.Interface.DataModels;
using Data.Interface.DataModels.AdminDataModels;
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

        public void EditNews(int id, string title, string text, string shorText)
        {
            var news = Get(id);
            news.Title = title;
            news.ShorText = shorText;
            news.Text = text;

            _webContext.SaveChanges();
        }
        public List<News> GetAllNewsWithIncludes()
        {
            return _dbSet.Include(x => x.Creator).ToList();
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

        public List<LastNews> GetLastNews()
        {
            var lastNews = _dbSet.Include(x => x.Creator).OrderByDescending(x => x.Id).Take(10).ToList();
            var data = lastNews.Select(x => new LastNews
            {
                Id = x.Id,
                Title = x.Title,
                Creator = new CreatorData
                {
                    Id = x.Creator.Id,
                    Name = x.Creator.Name
                }
            }).ToList();

            return data;
        }

        public EditNewsData GetNewsForEdit(int newsId)
        {
            var news = _dbSet.Include(x => x.NewsImages).FirstOrDefault(x => x.Id == newsId);
            var data = new EditNewsData
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                ShortText = news.ShorText
            };

            return data;
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
                        CreatedTime = x.CreatedTime,
                        Author = new CommentAuthor
                        {
                            Name = x.User.Name
                        }
                    }).ToList()
            };

            return data;
        }

        public override PaginatorData<News> GetPaginator(int page, int perPage)
        {
            var initialSource = _dbSet.Include(x => x.Creator);

            return base.GetPaginator(initialSource, page, perPage);
        }
    }
}
