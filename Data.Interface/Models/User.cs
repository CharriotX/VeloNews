namespace Data.Interface.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public virtual List<News> News { get; set; }
        public virtual List<Comment> NewsComments { get; set; }
    }
}
