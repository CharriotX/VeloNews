namespace VeloNews.Models.NewsViewModels
{
    public class EditNewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ShortText { get; set; }
        public List<string> NewsImageUrls { get; set; }
    }
}
