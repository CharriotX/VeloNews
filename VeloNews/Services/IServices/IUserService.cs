using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using VeloNews.Models;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Services.IServices
{
    public interface IUserService
    {
        void RegistrationUser(RegistrationUserViewModel viewModel);
        ProfileViewModel ShowProfile();
        EditMyProfileViewModel GetViewModelForEditProfilePage(int userId);
        EditMyProfileData EditMyProfile(EditMyProfileViewModel viewModel);
    }
}