using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Models
{
    public class NewsCategory : BaseModel
    {
        public string Name { get; set; }
        public virtual List<News> News { get; set; }
    }
}
