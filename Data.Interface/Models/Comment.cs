using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Models
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }

        public virtual News News { get; set; }

        public virtual User User { get; set; }
    }
}
