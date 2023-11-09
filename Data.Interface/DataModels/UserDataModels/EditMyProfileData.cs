using Data.Interface.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels.UserDataModels
{
    public class EditMyProfileData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserLanguage Language { get; set; }
    }
}
