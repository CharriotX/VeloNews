using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels.NewsDataModels
{
    public class AddNewsData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShorText { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }

        public NewsCommentAuthorData Author { get; set; }
        public List<NewsImageData> Images { get; set; }
    }


}
