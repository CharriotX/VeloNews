using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [EnglishWordFormInputs]
        [IsUserNameExist]
        public string Name { get; set; }

        [EnglishWordFormInputs]
        [IsPasswordCorrect]
        public string Password { get; set; }
    }
}
