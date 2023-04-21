using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels.NewsDataModels
{
    public class SaveNewsCommentData
    {
        public NewsId NewsId { get; set; }
        public string Text { get; set; }
        public Creator Author { get; set; }
    }

    public class NewsId
    {
        public int Id { get; set; }
    }

    public class Creator
    {
        public string Name { get; set; }
    }
}
