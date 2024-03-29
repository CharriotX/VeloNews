﻿using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using static Data.Interface.DataModels.HomeDateModels.HomePageData;

namespace Data.Interface.Repositories
{
    public interface INewsRepository : IBaseRepository<News>
    {
        List<News> GetAllNewsWithCreator();
        NewsWithCommentsAndImagesData GetNewsWithCommentsAndImages(int newsId);
        List<NewsCardsData> GetNewsByCategory(string categoryName);
        List<HomePageLastNewsData> GetNewsForHomePage();
        List<NewsCardsData> GetAllNewsCards();
        List<LastNewsData> GetLastNews();
        EditNewsData GetNewsForEdit(int newsId);
        void EditNews(EditNewsData data);
        int SaveNews(AddNewsData data);

    }
}
