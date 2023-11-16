using Data.Interface.DataModels.NewsDataModels;

namespace Data.Interface.DataModels.AdminDataModels
{
    public class NewsForAdminPageData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public NewsCommentAuthorData Creator { get; set; }
    }
}
