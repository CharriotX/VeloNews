using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VeloNews.Models.ValidationAttributes
{
    public class EnglishWordsFormInputsAttribute : ValidationAttribute
    {
        Regex pattern = new Regex("^[A-Za-z\\d_-]+$");
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var inputString = value == null ? String.Empty : value.ToString();
            MatchCollection matches = pattern.Matches(inputString);

            if (matches.Count > 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"You need use Eng words in {validationContext.DisplayName} field");
            }
        }
    }
}
