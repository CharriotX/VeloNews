namespace Data.Interface.Models
{
    public class UserProfileImage : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
    }
}
