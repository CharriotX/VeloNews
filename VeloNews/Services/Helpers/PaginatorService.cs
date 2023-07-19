using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models;

namespace VeloNews.Services.Helpers
{
    public class PaginatorService : IPaginatorService
    {
        public PaginatorViewModel<TViewModel> GetPaginatorViewModel<TViewModel, DbModel>(
            int page,
            int perPage,
            Func<DbModel, TViewModel> buildViewModelFunc,
            IBaseRepository<DbModel> repository)
            where TViewModel : class
            where DbModel : BaseModel
        {
            var dbPaginator = repository.GetPaginator(page, perPage);

            var viewModel = new PaginatorViewModel<TViewModel>();
            viewModel.Items = dbPaginator
                .Items
                .Select(buildViewModelFunc)
                .ToList();

            var doWeNeedMorePages = dbPaginator.TotalCount % perPage != 0;
            var totalPageCount = (dbPaginator.TotalCount / perPage)
                + (doWeNeedMorePages ? 1 : 0);
            viewModel.PagesListCount = totalPageCount;
            viewModel.ActivePageNumber = page;

            return viewModel;
        }
    }
}
