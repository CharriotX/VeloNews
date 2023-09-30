using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Sql.Repositories;
using System.ComponentModel.DataAnnotations;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsPasswordCorrectAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userRepository = validationContext.GetService(typeof(IUserRepository)) as UserRepository;
            var userContext = (LoginViewModel)validationContext.ObjectInstance;
            var password = value == null ? String.Empty : value.ToString();
            var user = userRepository.GetUserByUsername(userContext.Name);

            if (user == null)
            {
                return ValidationResult.Success;
            }

            if (user.Password != password)
            {
                return new ValidationResult("Incorrect password");
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}
