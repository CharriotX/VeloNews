using Data.Interface.Models;

namespace VeloNews.Services.IServices
{
    public interface INewsService
    {
        List<News> GetAllNews();
        News GetNewsWithComments(int id);
        void SaveComment(int newsId, string text);
    }
}
