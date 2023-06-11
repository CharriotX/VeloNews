using VeloNews.Models.ImageViewModels;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Models.NewsViewModels
{
    public class AddNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShorText { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public DateTime CreatedData { get; set; }
        public UserInfoViewModel Author { get; set; }
        public ImageViewModel Image { get; set; }
    }
}
