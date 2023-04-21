namespace VeloNews.Models.NewsViewModels
{
    public class NewsCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }

        public string PreviewImage { get; set; }
    }
}
