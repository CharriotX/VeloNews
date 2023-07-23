using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public CommentAuthor Author { get; set; }
    }

    public class CommentAuthor
    {
        public string Name { get; set; }
    }
}
