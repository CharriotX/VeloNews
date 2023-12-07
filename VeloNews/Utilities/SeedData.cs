using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;

namespace VeloNews.Utilities
{
    public static class SeedData
    {
        private const string ADMIN_DEFAULT_NAME_AND_PASSWORD = "admin";
        private const string DEFAULT_USER_PROFILE_IMAGE_NAME = "defaultUserImage";

        private const string SEED_USER_NAME = "SeedUser";
        private const string SEED_USER_PASSWORD = "1234";

        private const string RACE_CATEGORY_NAME = "Race";
        private const string NEWS_CATEGORY_NAME = "News";
        private const string RESULT_CATEGORY_NAME = "Result";

        public static void Seed(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                SeedAdmin(scope);
                SeedUsers(scope);
                SeedNewsCategory(scope);
                SeedNews(scope);
                SeedUserProfileImage(scope);
            }
        }

        private static void SeedAdmin(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var userProfileImageRepository = scope.ServiceProvider.GetRequiredService<IUserProfileImageRepository>();

            if (!userRepository.IsUserExist(ADMIN_DEFAULT_NAME_AND_PASSWORD))
            {
                var admin = new User()
                {
                    Name = ADMIN_DEFAULT_NAME_AND_PASSWORD,
                    Password = ADMIN_DEFAULT_NAME_AND_PASSWORD,
                    Role = UserRole.Admin,
                    Country = "Не указан",
                    DateOfBirth = new DateTime(01, 01, 01),
                    UserCreationDate = new DateTime(01, 01, 01)
                };

                userRepository.Save(admin);
            }
        }

        private static void SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            
            if (userRepository.Count() < 2)
            {

                for (var i = 0; i < 20; i++)
                {
                    var user = new User()
                    {
                        Name = SEED_USER_NAME + i,
                        Password = SEED_USER_PASSWORD,
                        Role = UserRole.User,
                        Country = "Не указан",
                        DateOfBirth = DateTime.Now,
                        UserCreationDate = DateTime.Now
                    };

                    userRepository.Save(user);
                }
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
            var imageRepository = scope.ServiceProvider.GetRequiredService<INewsImageRepository>();
            var newsCommentRepository = scope.ServiceProvider.GetRequiredService<INewsCommentRepository>();

            var resultCategory = newsCategoryRepository.GetCategoryByName(RESULT_CATEGORY_NAME);
            var newsCategory = newsCategoryRepository.GetCategoryByName(NEWS_CATEGORY_NAME);
            var raceCategory = newsCategoryRepository.GetCategoryByName(RACE_CATEGORY_NAME);

            var creator = userRepository.GetUserByUsername(ADMIN_DEFAULT_NAME_AND_PASSWORD);

            if (newsRepository.Count() < 5)
            {
                var countNews = newsRepository.Count();
                for (int i = countNews; i < 5; i++)
                {
                    var seedNews = new News()
                    {
                        Title = $"Seed news {i}",
                        ShorText = "38-летний британский велогонщик, четырёхкратный победитель Тур де Франс Крис Фрум надеется в следующем году вернуться на французский Гранд-тур, куда не смог попасть в составе Israel-Premier Tech в этом сезоне, о чём рассказал в интервью Marca.",
                        Text = "Крис Фрум: «Думаю, моя главная цель на следующий год - вернуться на свой лучший уровень на Тур де Франс. Особенно, учитывая, что Тур закончится там, где я живу. Последний этап проложен между Монако и Ниццей. Но всё зависит от множества обстоятельств, в том числе от того, что решит команда. Однако я очень надеюсь быть на Туре. Собираюсь работать изо всех сил, чтобы туда попасть. Теперь после моего ужасного инцидента я всё принимаю как подарок, счастлив просто ездить на велосипеде». Глава команды Israel-Premier Tech Сильван Адамс в подкасте RadioCycling резко высказался об отсутствии результатов Криса Фрума, который выступает в команде с 2021 года:\r\n\r\n \r\n\r\n«Разве можно верить тому, что он говорит? Разве имеет смысл человеку, выигравшему  семь Гранд-туров, заявлять, что его посадка в седле сместилась на дюйм, или что он там ещё говорил? Как можно верить кому-то, кто выпадает даже не на последних подъёмах, а на первых?",
                        Category = newsCategory,
                        Creator = creator,
                        CreatedTime = DateTime.Now,

                    };

                    newsRepository.Save(seedNews);

                    var thisNews = newsRepository.Get(seedNews.Id);
                    imageRepository.Save(new NewsImage()
                    {
                        Name = $"SeedImage{i}",
                        Url = $"/images/seedNewsImages/newsCategory/seed_news_img_0.png",
                        News = thisNews
                    });

                    for (int x = 0; x < 3; x++)
                    {
                        newsCommentRepository.Save(new Comment()
                        {
                            News = thisNews,
                            Text = $"Seed comment {x}",
                            User = creator,
                            CreatedTime = DateTime.Now
                        });
                    }
                }
                for (int i = countNews; i < 5; i++)
                {
                    var seedNews = new News()
                    {
                        Title = $"Seed news {i}",
                        ShorText = "Классика “Ломбардия” - финальная из пяти Монументов сезона, неизменно собирающая звёзд велоспорта, пройдёт в 117-й раз. «Гонка опавших листьев» благоволит горнякам, а своё расположение к ним проявляет 4400 метрами набора высоты и почти 240 километрами дистанции.\r\n\r\n \r\n\r\nТадей Погачар (UAE Team Emirates) поборется за третью подряд победу на Ломбардии, а его соперниками будут Примож Роглич (Jumbo-Visma), Ремко Эвенепул (Soudal Quick Step), Ричард Карапас (EF Education EasyPost) и другие. А Тибо Пино (Groupama-FDJ) на Ломбардии-2023 завершает карьеру профессионального велогонщика.",
                        Text = "Маршрут Ломбардии-2023 - полная копия  выпуска 2021 года, когда свою первую победу на этом Монументе одержал Тадей Погачар (UAE Team Emirates). Участники стартуют в городе Комо, чтобы через 238 км финишировать в Бергамо.\r\n\r\n \r\n\r\nПервым подъёмом дня через 30 км после старта будет восхождение к знаменитой церкви покровительницы велосипедистов - Мадонне дель Гизалло.\r\n\r\n \r\n\r\nОднако восхождение пройдёт по более пологой стороне подъёма - 8,8 км с 3,9%. Через 90 км после старта с подъёма Ронкола (Roncola) - 9,4 км  с 6,6% и участками, где градиент достигнет 17%, начнётся решающая часть классики, где подъёмы и спуски пойдут один за другим.",
                        Category = raceCategory,
                        Creator = creator,
                        CreatedTime = DateTime.Now,

                    };

                    newsRepository.Save(seedNews);

                    var thisNews = newsRepository.Get(seedNews.Id);

                    var imageNum = 0;
                    for (int x = 0; x < 2; x++)
                    {
                        imageRepository.Save(new NewsImage()
                        {
                            Name = $"SeedImage{imageNum}",
                            Url = $"/images/seedNewsImages/raceCategory/seed_race_img_{x}.jpg",
                            News = thisNews
                        });
                        imageNum++;
                    }


                    for (int x = 0; x < 3; x++)
                    {
                        newsCommentRepository.Save(new Comment()
                        {
                            News = thisNews,
                            Text = $"Seed comment {x}",
                            User = creator,
                            CreatedTime = DateTime.Now
                        });
                    }
                }
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
                    imageRepository.Save(new NewsImage()
                    {
                        Name = $"SeedImage{i}",
                        Url = $"/images/seedNewsImages/resultCategory/seed_result_img_{i}.png",
                        News = thisNews
                    });

                    for (int x = 0; x < 3; x++)
                    {
                        newsCommentRepository.Save(new Comment()
                        {
                            News = thisNews,
                            Text = $"Seed comment {x}",
                            User = creator,
                            CreatedTime = DateTime.Now
                        });
                    }
                }
            }
        }

        private static void SeedUserProfileImage(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var userProfileImageRepository = scope.ServiceProvider.GetRequiredService<IUserProfileImageRepository>();

            var users = userRepository.GetAll();

            if (!userProfileImageRepository.Any())
            {
                foreach (var user in users)
                {
                    userProfileImageRepository.Save(new UserProfileImage()
                    {
                        Name = DEFAULT_USER_PROFILE_IMAGE_NAME,
                        Url = $"/images/default-user-image.png",
                        User = user
                    });
                }
            }
        }
    }
}
