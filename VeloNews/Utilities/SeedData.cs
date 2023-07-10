using Data.Interface.Models;
using Data.Interface.Repositories;

namespace VeloNews.Utilities
{
    public static class SeedData
    {
        private const string ADMIN_DEFAULT_NAME_AND_PASSWORD = "admin";
        public static void Seed(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                SeedUsers(scope);
                SeedNewsCategory(scope);
                SeedNews(scope);
            }
        }

        private static void SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            if (!userRepository.IsUserExist(ADMIN_DEFAULT_NAME_AND_PASSWORD))
            {
                var admin = new User()
                {
                    Name = ADMIN_DEFAULT_NAME_AND_PASSWORD,
                    Password = ADMIN_DEFAULT_NAME_AND_PASSWORD,
                    Role = UserRole.Admin
                };

                userRepository.Save(admin);
            }
        }

        private static void SeedNewsCategory(IServiceScope scope)
        {
            var newsCategoryRepository = scope.ServiceProvider.GetRequiredService<INewsCategoryRepository>();

            if (!newsCategoryRepository.Any())
            {
                newsCategoryRepository.Save(new NewsCategory()
                {
                    Name = "News"
                });

                newsCategoryRepository.Save(new NewsCategory()
                {
                    Name = "Result"
                });

                newsCategoryRepository.Save(new NewsCategory()
                {
                    Name = "Race"
                });
            }
        }

        private static void SeedNews(IServiceScope scope)
        {
            var newsRepository = scope.ServiceProvider.GetRequiredService<INewsRepository>();
            var newsCategoryRepository = scope.ServiceProvider.GetRequiredService<INewsCategoryRepository>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var imageRepository = scope.ServiceProvider.GetRequiredService<IImageRepository>();

            var resultCategory = newsCategoryRepository.GetCategoryByName("result");
            var creator = userRepository.GetUserByUserName(ADMIN_DEFAULT_NAME_AND_PASSWORD);

            if (newsRepository.Count() < 5)
            {
                var countNews = newsRepository.Count();
                for (int i = countNews; i < 5; i++)
                {
                    var seedNews = new News()
                    {
                        Title = $"Seed news {i}",
                        ShorText = "Мадс Педерсен одержал победу на 8-м этапе Тур де Франс-2023, выиграв спринт в Лиможе.  27-летний датский гонщик команды Lidl Trek выиграл этап Большой Петли второй раз в карьере.",
                        Text = "8-й этап начался с многочисленных атак, но отрыв сформировался только через 20 км после ускорения Тима Деклерка (Tim Declercq/Soudal-Quick Step), к которому переложились Антни Тюржи (Anthony Turgis /TotalEnergies) и Антони Делаплас (Anthony Delaplace /Arkea-Samsic). Пелотон отпустил трио на 5 минут. После прохождения промежуточного спринта  Матье ван дер Пул (Alpecin-Deceuninck) попытался застать пелотон врасплох и атаковал. За ним образовалась группа из 14 гонщиков, но команда Jumbo-Visma закрыла просвет через пару километров.",
                        Category = resultCategory,
                        Creator = creator,
                        CreatedTime = DateTime.Now,

                    };

                    newsRepository.Save(seedNews);

                    var thisNews = newsRepository.Get(seedNews.Id);
                    imageRepository.Save(new Image()
                    {
                        Name = $"SeedImage{i}",
                        Url = $"/images/seedImages/seed_img_{i}.png",
                        News = thisNews
                    });
                }
            }
        }
    }
}
