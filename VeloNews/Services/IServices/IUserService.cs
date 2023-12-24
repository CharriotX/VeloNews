using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using VeloNews.Models;
using VeloNews.Models.UserViewModels;

namespace VeloNews.Services.IServices
{
    public interface IUserService
    {
        void RegistrationUser(RegistrationUserViewModel viewModel);
        MyProfileViewModel ShowMyProfile();
        UserProfileViewModel ShowUserProfile(int userId);
        EditMyProfileViewModel GetViewModelForEditProfilePage(int userId);
        EditMyProfileData EditMyProfile(EditMyProfileViewModel viewModel);
        PaginatorViewModel<UserInfoViewModel> UsersForAdminPage(int page, int perPage, string sortField);
        UserInfoViewModel BuildUserInfoViewModel(User dbUser);
        List<UserDataForLogin> GetLoginUsers();
    }
}