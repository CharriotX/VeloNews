using Data.Interface.Models.enums;

namespace Data.Interface.DataModels.UserDataModels
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string UserProfileImageUrl { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserLanguage Language { get; set; }
        public string Country { get; set; }
    }
}
