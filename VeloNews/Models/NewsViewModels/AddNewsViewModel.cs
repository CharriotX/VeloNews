using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<IFormFile> Images { get; set; }
        public List<string> ImageUrls { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
