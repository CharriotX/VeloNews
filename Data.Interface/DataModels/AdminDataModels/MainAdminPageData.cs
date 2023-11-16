using Data.Interface.DataModels.NewsDataModels;

namespace Data.Interface.DataModels.AdminDataModels
{
    public class MainAdminPageData
    {
        public List<LastNewsData> LastPublishedNews { get; set; }
        public List<LastCommentData> LastComments { get; set; }
        public List<LastRegisteredUserData> LastRegisteredUsers { get; set; }
        public SiteStatsData SiteStats { get; set; } 
    }

    public class LastNewsData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public NewsCommentAuthorData Creator { get; set; }
    }

    public class LastCommentData
    {
        public int Id { get; set; }
        public NewsCommentAuthorData Creator { get; set; }
        public string Text { get; set; }
        public int NewsId { get; set; }
    }

    public class LastRegisteredUserData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class SiteStatsData
    {
        public int NewsCount { get; set; }
        public int NewsCommentsCount { get; set; }
        public int UsersCount { get; set; }
    }
}
