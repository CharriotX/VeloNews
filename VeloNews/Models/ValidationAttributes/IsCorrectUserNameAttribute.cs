using Data.Interface.Models;
using System.ComponentModel.DataAnnotations;
using VeloNews.Services.IServices;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsCorrectUserNameAttribute : ValidationAttribute
    {
        List<string> banWords = new List<string>() { "adminka", "admin", "administrator", "moderator", "moder", "god" };
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userService = validationContext.GetService<IUserService>();
            var userName = value == null ? String.Empty : value.ToString().ToLower();
            var currentUser = userService.GetCurrentUser();

            var incorrectUserName = banWords.Contains(userName);
            var isCurrentUserAdmin = banWords.Contains(currentUser.Name);


            if (userName == null)
            {
                return ValidationResult.Success;
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
