using Data.Interface.DataModels.AdminDataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeloNews.Models;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;

namespace VeloNews.Services
{
    [AutoDiServiceRegistration]
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUserProfileImageRepository _userProfileImageRepository;
        private IUserProfileImageService _userProfileImageService;
        private IPaginatorService _paginatorService;
        private IAuthenticationService _authenticationService;

        public UserService(IUserRepository userRepository,
            IUserProfileImageRepository userProfileImageRepository,
            IUserProfileImageService userProfileImageService,
            IAuthenticationService authenticationService,
            IPaginatorService paginatorService)
        {
            _userRepository = userRepository;
            _userProfileImageRepository = userProfileImageRepository;
            _userProfileImageService = userProfileImageService;
            _authenticationService = authenticationService;
            _paginatorService = paginatorService;
        }

        public void RegistrationUser(RegistrationUserViewModel viewModel)
        {
            var data = new UserRegistrationData()
            {
                UserName = viewModel.UserName,
                Password = viewModel.Password
            };

            var savedUser = _userRepository.UserRegistration(data);

            _userProfileImageRepository.SaveDefaultUserProfileImage(savedUser);
        }

        public List<UserDataForLogin> GetLoginUsers()
        {
            var users = _userRepository.GetUsersForLogin();

            return users;
        }

        public EditMyProfileViewModel GetViewModelForEditProfilePage(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            var countries = ISO3166.Country.List.OrderBy(x => x.Name).ToList();
            var languages = Enum.GetValues(typeof(UserLanguage)).Cast<UserLanguage>();

            var viewModel = new EditMyProfileViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Countries = countries.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name
                }).ToList(),
                Languages = languages.Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Value = ((int)x).ToString()
                }).ToList()
            };

            return viewModel;
        }

        public EditMyProfileData EditMyProfile(EditMyProfileViewModel viewModel)
        {
            var data = new EditMyProfileData
            {
                Id = viewModel.UserId,
                Name = viewModel.Name,
                Country = viewModel.Country,
                DateOfBirth = viewModel.DateOfBirth,
                Language = viewModel.Language
            };

            _userRepository.EditMyProfile(data);

            if (viewModel.NewProfileImage != null)
            {
                _userProfileImageService.UploadNewProfileImage(viewModel.NewProfileImage, viewModel.UserId);
            }
            return data;
        }

        public MyProfileViewModel ShowMyProfile()
        {
            var currentUser = _authenticationService.GetCurrentUserData();
            var userData = _userRepository.GetUserProfileData(currentUser.Id);

            var model = new MyProfileViewModel()
            {
                Id = userData.User.Id,
                Name = userData.User.Name,
                Role = userData.User.Role,
                ProfileImageUrl = userData.User.UserProfileImageUrl,
                DateOfBirth = userData.User.DateOfBirth,
                UserCreationDate = userData.User.UserCreationDate,
                Country = userData.User.Country,
                Language = userData.User.Language
            };

            return model;
        }

        public UserProfileViewModel ShowUserProfile(int userId)
        {
            var userData = _userRepository.GetUserProfileData(userId);

            var model = new UserProfileViewModel()
            {
                Id = userData.User.Id,
                Name = userData.User.Name,
                ProfileImageUrl = userData.User.UserProfileImageUrl,
                DateOfBirth = userData.User.DateOfBirth,
                UserCreationDate = userData.User.UserCreationDate,
                Country = userData.User.Country
            };

            return model;
        }

        public PaginatorViewModel<UserInfoViewModel> UsersForAdminPage(int page, int perPage, string sortField)
        {
            var viewModel = _paginatorService.GetPaginatorViewModel(
                page,
                perPage,
                BuildUserInfoViewModel,
                _userRepository,
                sortField);

            return viewModel;
        }

        public UserInfoViewModel BuildUserInfoViewModel(User dbUser)
        {
            return new UserInfoViewModel()
            {
                Id = dbUser.Id,
                Name = dbUser.Name,
                UserCreationDate = dbUser.UserCreationDate,
                Country = dbUser.Country,
                Role = dbUser.Role.ToString()
            };
        }
    }
}
