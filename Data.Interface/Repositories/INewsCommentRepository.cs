using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface INewsCommentRepository : IBaseRepository<Comment>
    {
        void SaveComment(SaveNewsCommentData data);
        List<LastComment> GetLastComments();
    }
}
