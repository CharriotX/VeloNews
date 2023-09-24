using Data.Interface.DataModels;
using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using VeloNews.Models;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;
using static System.Net.Mime.MediaTypeNames;

namespace VeloNews.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserProfileImageRepository _userProfileImageRepository;
        private IUserProfileImageService _userProfileImageService;

        public UserService(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserProfileImageRepository userProfileImageRepository,
            IUserProfileImageService userProfileImageService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userProfileImageRepository = userProfileImageRepository;
            _userProfileImageService = userProfileImageService;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetCurrentUser()
        {
            var idStr = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(x => x.Type == "Id")
                ?.Value;

            if (idStr == null)
            {
                return null;
            }

            var id = int.Parse(idStr);
            var user = _userRepository.Get(id);

            return user;
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

        public User GetUserByNameAndPass(string userName, string userPass)
        {
            return _userRepository.GetUserByNameAndPass(userName, userPass);
        }


        public EditMyProfileViewModel GetViewModelForEditProfilePage(int userId)
        {
            var user = _userRepository.GetUserWithProfileImage(userId);
            var countries = ISO3166.Country.List.OrderBy(x => x.Name).ToList();

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
                DateOfBirth = viewModel.DateOfBirth
            };

            _userRepository.EditMyProfile(data);

            if (viewModel.NewProfileImage != null)
            {
                _userProfileImageService.UploadNewProfileImage(viewModel.NewProfileImage, viewModel.UserId);
            }
            return data;
        }

        public ProfileViewModel ShowProfile()
        {
            var currentUser = GetCurrentUser();
            var userData = _userRepository.GetUserProfileData(currentUser.Id);

            var model = new ProfileViewModel()
            {
                Id = userData.User.Id,
                Name = userData.User.UserName,
                Role = userData.User.Role,
                ProfileImageUrl = userData.User.UserProfileImageUrl,
                DateOfBirth = userData.User.DateOfBirth,
                UserCreationDate = userData.User.UserCreationDate,
                Country = userData.User.Country
            };

            return model;
        }
    }
}
