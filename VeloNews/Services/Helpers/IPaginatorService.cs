using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models;

namespace VeloNews.Services.Helpers
{
    public interface IPaginatorService
    {
        PaginatorViewModel<TViewModel> GetPaginatorViewModel<TViewModel, DbModel>(int page, int perPage, Func<DbModel, TViewModel> buildViewModelFunc, IBaseRepository<DbModel> repository)
            where TViewModel : class
            where DbModel : BaseModel;
    }
}