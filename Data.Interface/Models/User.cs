using Data.Interface.Models.enums;

namespace Data.Interface.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string Country { get; set; }
        public UserLanguage Language { get; set; }
        public virtual List<News> News { get; set; }
        public virtual List<Comment> NewsComments { get; set; }
        public virtual UserProfileImage UserProfileImage { get; set; }
    }
}
