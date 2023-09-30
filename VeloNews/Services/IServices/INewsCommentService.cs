using Data.Interface.DataModels.NewsDataModels;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsCommentService
    {
        SaveNewsCommentViewModel SaveComment(int commentId, int newsId, string text);
        SaveNewsCommentViewModel EditComment(int commentId, string text);
        void RemoveComment(int commentId);
    }
}
