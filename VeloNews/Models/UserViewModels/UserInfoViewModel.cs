namespace VeloNews.Models.UserViewModels
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string Country { get; set; }
    }
}
