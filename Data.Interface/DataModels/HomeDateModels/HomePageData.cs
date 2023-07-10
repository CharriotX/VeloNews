using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels.HomeDateModels
{
    public class HomePageData
    {
        public class HomePageLastNewsData
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ShortText { get; set; }
            public string Category { get; set; }
            public string PreviewImageUrl { get; set; }
        }
    }
}
