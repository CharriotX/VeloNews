using System.ComponentModel.DataAnnotations;

namespace VeloNews.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortText { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; } = "author";

        [Required]
        public List<IFormFile> NewsImages { get; set; }
    }
}
