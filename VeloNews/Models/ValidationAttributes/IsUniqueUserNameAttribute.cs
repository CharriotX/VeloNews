using Data.Interface.Repositories;
using Data.Sql.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsUniqueUserNameAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"{name} cant be empty"
                : ErrorMessage;
        }
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            var userRepository = validationContext.GetService(typeof(IUserRepository)) as UserRepository;

            var userName = value?.ToString();

            var isDuplicate = userRepository.IsUserNameExist(userName);
            if (isDuplicate)
            {
                return new ValidationResult("This user name is already used");
            }

            return ValidationResult.Success;
        }
    }
}
