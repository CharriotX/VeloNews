using Data.Interface.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels
{
    public class UserData
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string UserProfileImageUrl { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserLanguage Language { get; set; }
        public string Country { get; set; }
    }
}
