namespace VeloNews.Models.NewsViewModels
{
    public class LastNewsCommentsViewModel
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string Text { get; set; }
        public string Creator { get; set; }
    }
}
