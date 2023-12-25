using System.ComponentModel.DataAnnotations;
using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class RegistrationUserViewModel
    {
        [EnglishWordsFormInputs]
        [IsCorrectRegisterUserName]
        [IsUniqueRegisterUserName]
        [MinLength(4, ErrorMessage = "Minimum username length 4 characters")]
        public string UserName { get; set; }

        [MinLength(4, ErrorMessage = "Minimum password length 4 characters")]
        [EnglishWordsFormInputs]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Confirm password doesn't match, type again!")]
        public string ConfirmPassword { get; set; }
    }
}
