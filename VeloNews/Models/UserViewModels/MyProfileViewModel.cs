using Data.Interface.Models.enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class MyProfileViewModel
    {
        public int Id { get; set; }
        [IsUniqueUserName]
        [IsCorrectUserName]
        public string Name { get; set; }
        public string Role { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Country { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DefaultDateOfBirth { get; set; } = new DateTime(1500, 01, 01);
        public UserLanguage Language { get; set; }
    }
}
