namespace VeloNews.Models
{
    public class ShowNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }

        public List<string> NewsUrlsImages { get; set; }
        public List<NewsCommentViewModel> NewsComments { get; set; } 
    }
}
