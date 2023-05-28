namespace VeloNews.Models
{
    public class PaginatorViewModel<T> where T : class
    {
        public List<T> Items { get; set; }
        public int ActivePageNumber { get; set; }
        public List<int> PageList { get; set; }
    }
}
