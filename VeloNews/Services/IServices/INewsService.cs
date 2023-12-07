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
        PaginatorViewModel<NewsCardViewModel> GetNewsByCategoryWithPagination(string categoryName, int page);
        EditNewsViewModel GetNewsForEdit(int newsId);
        AddNewsData SaveNews(AddNewsViewModel viewModel);
        AddNewsViewModel GetAllNewsCategories();
        HomeViewModel GetNewsForHomePage();
        PaginatorViewModel<NewsForAdminPageViewModel> GetAllNewsForAdminPagginator(int page, int perPage, string sortField);
        NewsForAdminPageViewModel BuildAdminNewsViewModel(News dbNews);
        void EditNews(EditNewsViewModel viewModel);
        void DeleteNews(int id);
    }
}
