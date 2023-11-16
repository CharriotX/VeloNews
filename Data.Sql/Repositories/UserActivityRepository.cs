using Data.Interface.DataModels.UserActivityHub;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class UserActivityRepository : BaseRepository<UserActivityHub>, IUserActivityRepository
    {
        public UserActivityRepository(WebContext webContext) : base(webContext)
        {
        }

        public void SaveCommentActivityHistory(string username, string newsId, string text)
        {
            var dbModel = new UserActivityHub()
            {
                Username = username,
                Description = $"оставил комментарий <a href=\"News/ShowNews/{newsId}\">к новости</a> с текстом: {text}"
            };

            Save(dbModel);
        }

        public void SaveAutorizationActivityHistory(string username, string description)
        {
            var dbModel = new UserActivityHub()
            {
                Username = username,
                Description = description
            };

            Save(dbModel);
        }

        public List<UserActivityData> GetLastUserActivityHistory()
        {
            var dataModels = _dbSet
                .OrderByDescending(x => x.Id)
                .Take(30)
                .Select(dbModel => new UserActivityData
                {
                    Id = dbModel.Id,
                    Username = dbModel.Username,
                    Description = dbModel.Description
                })
                    .ToList();

            return dataModels;
        }
    }
}
