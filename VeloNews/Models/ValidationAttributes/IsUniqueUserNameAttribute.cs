using Data.Interface.Repositories;
using Data.Sql.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Data;
using VeloNews.Services;
using VeloNews.Services.IServices;

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
            var authService = validationContext.GetService(typeof(IAuthenticationService)) as AuthenticationService;

            var userName = value?.ToString();

            var currentUser = authService.GetCurrentUserData();

            var isDuplicate = userRepository.IsUserNameExist(userName);

            bool IsEditUserName = false;

            if (currentUser.Name == userName)
            {
                IsEditUserName = true;
            }

            if (isDuplicate & !IsEditUserName)
            {
                return new ValidationResult("This user name is already used");
            }

            return ValidationResult.Success;
        }
    }
}
