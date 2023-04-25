using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class RegistrationUserViewModel
    {
        [IsUniqueUserName]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
