using Data.Interface.DataModels.UserDataModels;

namespace Data.Interface.DataModels.NewsDataModels
{
    public class NewsWithCommentsAndImagesData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Author { get; set; }

        public List<ImageUrlsForShowNews> NewsUrlsImages { get; set; }

        public List<CommentsNews> NewsComments { get; set; }
    }

    public class ImageUrlsForShowNews
    {
        public string Url { get; set; }
    }

    public class CommentsNews
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Text { get; set; }
        public UserData Author { get; set; }
    }
}
