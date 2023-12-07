namespace VeloNews.Models.UserViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Country { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
