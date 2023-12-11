using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeloNews.Models.AdminViewModels;
using VeloNews.Models;
using VeloNews.Models.HomeViewModels;
using VeloNews.Models.NewsViewModels;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;
using VeloNews.Services.ServiceAttributes;
using Data.Interface.Models.enums;

namespace VeloNews.Services
{
    [AutoDiServiceRegistration]
    public class NewsService : INewsService
    {
        private INewsRepository _newsRepository;
        private INewsImageRepository _imageRepository;
        private INewsCategoryRepository _newsCategoryRepository;
        private IPaginatorService _paginatorService;
        private IAuthenticationService _authenticationService;
        private INewsImageService _newsImageService;

        public NewsService(INewsRepository newsRepository,
            INewsImageRepository imageRepository,
            INewsCategoryRepository newsCategoryRepository,
            IPaginatorService paginatorService,
            IAuthenticationService authenticationService,
            INewsImageService newsImageService)
        {
            _newsRepository = newsRepository;
            _imageRepository = imageRepository;
            _newsCategoryRepository = newsCategoryRepository;
            _paginatorService = paginatorService;
            _authenticationService = authenticationService;
            _newsImageService = newsImageService;
        }

        public void EditNews(EditNewsViewModel viewModel)
        {
            var data = new EditNewsData
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Text = viewModel.Text,
                ShortText = viewModel.ShortText,
                CreatedTime = DateTime.Now
            };

            _newsRepository.EditNews(data);
        }

        public PaginatorViewModel<NewsCardViewModel> GetNewsByCategoryWithPagination(string categoryName, int page)
        {
            var newsByCategory = _newsRepository.GetNewsByCategory(categoryName);

            var perPage = 3f;
            var pageCount = Math.Ceiling(newsByCategory.Count() / perPage);

            var news = newsByCategory
                .Skip((page - 1) * (int)perPage)
                .Take((int)perPage)
                .ToList();

            var viewModel = new PaginatorViewModel<NewsCardViewModel>()
            {
                ActivePageNumber = page,
                PagesListCount = (int)pageCount,
                Items = news.Select(x => new NewsCardViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Category = x.Category,
                    CreatedTime = x.CreatedTime,
                    PreviewImage = x.PreviewImage,
                    ShortText = x.ShortText
                }).ToList()
            };

            return viewModel;
        }

        public HomeViewModel GetNewsForHomePage()
        {
            var lastNews = _newsRepository.GetNewsForHomePage();

            var model = new HomeViewModel()
            {
                LastNews = lastNews.Select(x => new HomePageNewsViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortText = x.ShortText,
                    Category = x.Category,
                    PreviewImage = x.PreviewImageUrl
                }).ToList()
            };

            return model;
        }

        public List<NewsCardViewModel> GetAllNewsCards()
        {
            var allNews = _newsRepository.GetAllNewsCards();

            var models = allNews.Select(model => new NewsCardViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ShortText = model.ShortText,
                CreatedTime = model.CreatedTime,
                Author = model.Author,
                Category = model.Category,
                PreviewImage = model.PreviewImage
            }).Reverse().ToList();

            return models;
        }

        public AddNewsViewModel GetAllNewsCategories()
        {
            var categories = _newsCategoryRepository.GetAll();
            var model = new AddNewsViewModel()
            {
                Categories = categories.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };

            return model;
        }

        public ShowNewsViewModel GetFullNews(int newsId)
        {
            var news = _newsRepository.GetNewsWithCommentsAndImages(newsId);

            if (news == null)
            {
                return null;
            }

            var model = new ShowNewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                ShortText = news.ShortText,
                CreatedTime = news.CreatedTime,
                Author = news.Author,
                NewsUrlsImages = news.NewsUrlsImages.Select(x => new NewsImageViewModel()
                {
                    Url = x.Url
                }).ToList(),
                NewsComments = news.NewsComments.Select(x => new NewsCommentViewModel()
                {
                    Id = x.Id,
                    UserId = x.User.Id,
                    UserName = x.User.Name,
                    UserAvatarUrl = x.User.UserProfileImageUrl,
                    CreatedTime = x.CreatedTime,
                    Text = x.Text
                }).Reverse().ToList()
            };

            return model;
        }

        public EditNewsViewModel GetNewsForEdit(int newsId)
        {
            var news = _newsRepository.GetNewsForEdit(newsId);
            var viewModel = new EditNewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Text = news.Text,
                ShortText = news.ShortText
            };

            return viewModel;
        }

        public AddNewsData SaveNews(AddNewsViewModel viewModel)
        {
            var user = _authenticationService.GetCurrentUserData();

            var data = new AddNewsData
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                ShorText = viewModel.ShorText,
                Text = viewModel.Text,
                CreatedDate = DateTime.Now,
                CategoryId = viewModel.SelectedCategoryId,
                Author = new NewsCommentAuthorData
                {
                    Id = user.Id,
                    AuthorName = user.Name
                }
            };

            var thisNewsId = _newsRepository.SaveNews(data);

            _newsImageService.UploadNewsImages(thisNewsId, viewModel.Images, data.CreatedDate);

            return data;
        }

        public PaginatorViewModel<NewsForAdminPageViewModel> GetAllNewsForAdminPagginator(int page, int perPage, string sortField)
        {
            var viewModel = _paginatorService.GetPaginatorViewModel(
                page,
                perPage,
                BuildAdminNewsViewModel,
                _newsRepository,
                sortField
                );

            return viewModel;
        }

        public NewsForAdminPageViewModel BuildAdminNewsViewModel(News dbNews)
        {
            return new NewsForAdminPageViewModel()
            {
                Id = dbNews.Id,
                CreatorName = dbNews.Creator.Name,
                TimeOfCreation = dbNews.CreatedTime,
                Title = dbNews.Title
            };
        }

        public void DeleteNews(int id)
        {
            _newsRepository.Remove(id);
        }
    }
}
