namespace VeloNews.Models.NewsViewModels
{
    public class SaveNewsCommentViewModel
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string Text { get; set; }
        public string CreatedTime { get; set; }
        public string AuthorProfileImageUrl { get; set; }
        public string Author { get; set; }
    }
}
