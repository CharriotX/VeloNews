using Data.Interface.DataModels.UserActivityHub;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserActivityRepository : IBaseRepository<UserActivityHub>
    {
        void SaveCommentActivityHistory(string username,string newsId, string text);
        void SaveAutorizationActivityHistory(string username, string description);
        List<UserActivityData> GetLastUserActivityHistory();
    }
}
