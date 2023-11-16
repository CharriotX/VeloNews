namespace Data.Interface.Models
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual News News { get; set; }

        public virtual User User { get; set; }
    }
}
