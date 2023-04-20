using Data.Interface.DataModels;
using Data.Interface.Models;
using VeloNews.Models;

namespace VeloNews.Services.IServices
{
    public interface INewsService
    {
        List<News> GetAllNews();
        ShowNewsViewModel GetFullNews(int newsId);
        News GetNewsWithComments(int id);
        void SaveComment(int newsId, string text);
    }
}
