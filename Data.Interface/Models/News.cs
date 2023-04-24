using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Interface.Models
{
    public class News : BaseModel
    {
        public string Title { get; set; }
        public string ShorText { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual User Creator { get; set; }
        public virtual List<Image> NewsImages { get; set; }
        public virtual List<Comment> NewsComments { get; set; }
    }
}
