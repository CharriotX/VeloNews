using Data.Interface.Models;

namespace VeloNews.Services
{
    public interface INewsService
    {
        List<News> GetAllNews();
    }
}
