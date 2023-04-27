using System.ComponentModel.DataAnnotations;
using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class RegistrationUserViewModel
    {
        [IsCorrectUserName]
        [IsUniqueUserName]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Confirm password doesn't match, type again!")]
        public string ConfirmPassword { get; set; }
    }
}
