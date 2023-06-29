using Data.Interface.Models;
using Data.Interface.Repositories;

namespace VeloNews.Utilities
{
    public static class SeedData
    {
        public static void Seed(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                SeedUsers(scope);
                SeedNewsCategory(scope);
            }
        }

        private static void SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            if (!userRepository.IsUserExist("admin"))
            {
                var admin = new User()
                {
                    Name = "admin",
                    Password = "admin",
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
                    Name = "Race"
                });

                newsCategoryRepository.Save(new NewsCategory()
                {
                    Name = "Result"
                });
            }
        }
    }
}
