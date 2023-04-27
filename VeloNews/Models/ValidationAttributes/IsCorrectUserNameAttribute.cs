using Data.Interface.Models;
using System.ComponentModel.DataAnnotations;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsCorrectUserNameAttribute : ValidationAttribute
    {
        List<string> banWords = new List<string>() {"adminka", "admin", "administrator", "moderator", "moder", "god", "master" };
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userName = value == null ? String.Empty : value.ToString().ToLower();

            var incorrectUserName = banWords.Contains(userName);

            if (userName == null)
            {
                return ValidationResult.Success;
            }

            if (incorrectUserName)
            {
                return new ValidationResult("Forbidden username, please enter another!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
