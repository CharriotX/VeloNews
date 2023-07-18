using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using VeloNews.Models.AdminViewModels;
using VeloNews.Models;
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
        PaginatorViewModel<NewsForAdminPageViewModel> GetAllNewsForPagginator(int page, int perPage);
        PaginatorViewModel<NewsCardViewModel> GetNewsCardForPaginator(int page, int perPage);
        NewsForAdminPageViewModel BuildAdminNewsViewModel(News dbNews);
        NewsCardViewModel BuildNewsCardViewModel(News model);
        void EditNews(int id, string title, string text, string shorText);
    }
}
