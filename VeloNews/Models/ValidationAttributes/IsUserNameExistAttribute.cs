using Data.Interface.Repositories;
using Data.Sql.Repositories;
using System.ComponentModel.DataAnnotations;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsUserNameExistAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userRepository = validationContext.GetService(typeof(IUserRepository)) as UserRepository;

            var userName = value?.ToString();

            var isUserExist = userRepository.IsUserExist(userName);

            if (isUserExist)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("We didn't find an account for this username.");
            }
        }
    }
}
