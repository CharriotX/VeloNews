using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels.AdminDataModels
{
    public class NewsForAdminPageData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public CreatorData Creator { get; set; }
    }
}
