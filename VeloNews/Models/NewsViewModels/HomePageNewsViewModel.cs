using Microsoft.AspNetCore.Mvc.Rendering;
using VeloNews.Models.ImageViewModels;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Models.NewsViewModels
{
    public class HomePageNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string Category { get; set; }
        public string PreviewImage { get; set; }
    }
}
