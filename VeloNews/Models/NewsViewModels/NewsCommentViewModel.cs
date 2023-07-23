namespace VeloNews.Models.NewsViewModels
{
    public class NewsCommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }
    }
}
