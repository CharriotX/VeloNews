using Data.Interface.DataModels.NewsDataModels;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsCommentService
    {
        void SaveComment(SaveNewsCommentViewModel viewModel);
    }
}
