using System.ComponentModel.DataAnnotations;
using VeloNews.Services.IServices;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsCorrectRegisterUserNameAttribute : ValidationAttribute
    {
        List<string> banWords = new List<string>() { "adminka", "admin", "administrator", "moderator", "moder", "god" };
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var authService = validationContext.GetService<IAuthenticationService>();
            var userName = value == null ? String.Empty : value.ToString().ToLower();

            var incorrectUserName = banWords.Contains(userName);

            if (userName == null)
            {
                return ValidationResult.Success;
            }

            if (!incorrectUserName)
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
