using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }

        [EnglishWordsFormInputs]
        [IsUserNameExist]
        public string Name { get; set; }

        [EnglishWordsFormInputs]
        [IsPasswordCorrect]
        public string Password { get; set; }
    }
}
