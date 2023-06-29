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
    }
}
