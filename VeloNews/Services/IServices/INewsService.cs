using Data.Interface.DataModels;
using Data.Interface.Models;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsService
    {
        ShowNewsViewModel GetFullNews(int newsId);
        void SaveComment(int newsId, string text);
        List<NewsCardViewModel> GetAllNewsCards();
    }
}
