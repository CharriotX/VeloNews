namespace VeloNews.Models.NewsViewModels
{
    public class SaveNewsCommentViewModel
    {
        public int NewsId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }
    }
}
