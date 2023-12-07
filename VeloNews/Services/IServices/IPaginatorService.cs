using Data.Interface.Models;
using Data.Interface.Repositories;
using VeloNews.Models;

namespace VeloNews.Services.IServices
{
    public interface IPaginatorService
    {
        PaginatorViewModel<TViewModel> GetPaginatorViewModel<TViewModel, DbModel>(
            int page,
            int perPage,
            Func<DbModel, TViewModel> buildViewModelFunc,
            IBaseRepository<DbModel> repository,
            string sortField)
                where TViewModel : class
                where DbModel : BaseModel;
    }
}