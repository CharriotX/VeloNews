using Data.Interface.DataModels.NewsDataModels;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsCommentService
    {
        SaveNewsCommentViewModel SaveComment(int newsId, string text);
        void RemoveComment(int commentId);
    }
}
