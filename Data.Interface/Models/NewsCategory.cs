namespace Data.Interface.Models
{
    public class NewsCategory : BaseModel
    {
        public string Name { get; set; }
        public virtual List<News> News { get; set; }
    }
}
