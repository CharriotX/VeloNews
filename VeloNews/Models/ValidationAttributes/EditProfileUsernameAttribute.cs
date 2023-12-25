using Data.Interface.Repositories;
using Data.Sql.Repositories;
using System.ComponentModel.DataAnnotations;
using VeloNews.Services.IServices;

namespace VeloNews.Models.ValidationAttributes
{
    public class EditProfileUsernameAttribute : ValidationAttribute
    {
        List<string> banWords = new List<string>() { "adminka", "admin", "administrator", "moderator", "moder", "god" };
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var authService = validationContext.GetService<IAuthenticationService>();
            var userRepository = validationContext.GetService(typeof(IUserRepository)) as UserRepository;

            var userName = value == null ? String.Empty : value.ToString().ToLower();
            var currentUser = authService.GetCurrentUserData();

            var incorrectUserName = banWords.Contains(userName);
            var isCurrentUserAdmin = banWords.Contains(currentUser.Name);

            var isDuplicate = userRepository.IsUserNameExist(userName);

            if (userName == currentUser.Name.ToLower() && isDuplicate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("This user name is already used");
            }

            if (incorrectUserName == isCurrentUserAdmin)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Forbidden username, please enter another!");
            }
        }
    }
}
