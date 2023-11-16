namespace Data.Interface.DataModels.NewsDataModels
{
    public class NewsCardsData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string PreviewImage { get; set; }
    }

    public class PreviewImage
    {
        public string Url { get; set; }
    }
}
