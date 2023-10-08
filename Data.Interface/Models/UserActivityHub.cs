using Data.Interface.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Models
{
    public class UserActivityHub : BaseModel
    {
        public string Username { get; set; }
        public string Description { get; set; }
    }
}
