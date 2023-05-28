using Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels
{
    public class PaginatorData<T> where T: BaseModel
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
    }
}
