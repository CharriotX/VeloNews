using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using VeloNews.Models.HomeViewModels;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsService
    {
        ShowNewsViewModel GetFullNews(int newsId);
        List<NewsCardViewModel> GetAllNewsCards();
        EditNewsViewModel GetNewsForEdit(int newsId);
        AddNewsData SaveNews(AddNewsViewModel viewModel);
        AddNewsViewModel GetAllNewsCategories();
        HomeViewModel GetNewsForHomePage();
        void EditNews(int id, string title, string text, string shorText);
    }
}
