using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels
{
    public class ImageData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int NewsId { get; set; }
    }
}
