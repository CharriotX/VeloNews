using Data.Interface.DataModels.NewsDataModels;
using VeloNews.Models.NewsViewModels;

namespace VeloNews.Services.IServices
{
    public interface INewsCommentService
    {
        SaveNewsCommentViewModel SaveComment(SaveNewsCommentApiData dataComment);
        SaveNewsCommentViewModel EditComment(SaveNewsCommentApiData data);
        void RemoveComment(int commentId);
    }
}
