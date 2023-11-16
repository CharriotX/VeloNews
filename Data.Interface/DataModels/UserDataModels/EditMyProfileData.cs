using Data.Interface.Models.enums;

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
