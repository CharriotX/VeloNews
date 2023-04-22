using Data.Interface.DataModels.NewsDataModels;

namespace VeloNews.Services.IServices
{
    public interface INewsCommentService
    {
        void SaveComment(int newsId, string text);
    }
}
