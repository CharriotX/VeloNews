using VeloNews.Models.UserViewModels;

namespace VeloNews.Models.AdminViewModels
{
    public class NewsForAdminPageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public UserInfoViewModel Creator { get; set; }
    }
}
