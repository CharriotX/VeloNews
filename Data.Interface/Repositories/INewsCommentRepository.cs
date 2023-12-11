using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface INewsCommentRepository : IBaseRepository<Comment>
    {
        int SaveComment(SaveNewsCommentData data);
        NewsCommentAuthorData GetCommentAuthor(int id);
        List<LastCommentData> GetLastComments();
    }
}
