namespace Data.Interface.DataModels.NewsDataModels
{
    public class EditNewsData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ShortText { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }

        public List<NewsImageUrl> NewsImageUrls { get; set; }
    }

    public class NewsImageUrl
    {
        public string Url { get; set; }
    }
}
