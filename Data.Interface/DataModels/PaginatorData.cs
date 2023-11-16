using Data.Interface.Models;

namespace Data.Interface.DataModels
{
    public class PaginatorData<T> where T: BaseModel
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
    }
}
