using Data.Interface.DataModels.NewsDataModels;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsCommentService
    {
        SaveNewsCommentViewModel SaveComment(SaveNewsCommentApiData dataComment);
        SaveNewsCommentViewModel EditComment(SaveNewsCommentApiData data);
        bool UserIsAuthor(int commentId);
        void RemoveComment(int commentId);
    }
}
