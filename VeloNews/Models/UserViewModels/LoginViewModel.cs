using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [IsUserNameExist]
        public string Name { get; set; }

        [IsPasswordCorrect]
        public string Password { get; set; }
    }
}
