namespace VeloNews.Models
{
    public class PaginatorViewModel<T> where T : class
    {
        public List<T> Items { get; set; }
        public int ActivePageNumber { get; set; }
        public int PagesListCount { get; set; }

        public bool ShowPrevious => ActivePageNumber > 1;
        public bool ShowNext => ActivePageNumber < PagesListCount;
        public bool ShowFirst => ActivePageNumber != 1;
        public bool ShowLast => ActivePageNumber != PagesListCount;
    }
}
