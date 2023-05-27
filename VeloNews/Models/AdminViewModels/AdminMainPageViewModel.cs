using VeloNews.Models.NewsViewModels;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Models.AdminViewModels
{
    public class AdminMainPageViewModel
    {
        public List<LastNewsViewModel> LastNews { get; set; }
        public List<LastNewsCommentsViewModel> LastNewsComments { get; set; }
        public List<LastRegisteredUsersViewModel> LastRegisteredUsers { get; set; }
        public SiteStatsViewModel SiteStats { get; set; }
    }
}
