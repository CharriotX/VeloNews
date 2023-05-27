using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface INewsRepository : IBaseRepository<News>
    {
        News GetNewsWithComments(int newsId);
        NewsWithCommentsAndImagesData GetNewsWithCommentsAndImages(int newsId);
        List<NewsCardsData> GetAllNewsCards();
        List<LastNews> GetLastNews();
        EditNewsData GetNewsForEdit(int newsId);
        void EditNews(int id, string title, string text, string shorText);
    }
}
