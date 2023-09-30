using Data.Interface.DataModels;
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

namespace VeloNews.Services
{
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

        public void EditNews(int id, string title, string text, string shorText)
        {
            _newsRepository.EditNews(id, title, text, shorText);
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
                    UserName = x.Author.UserName,
                    AuthorAvatarUrl = x.Author.UserProfileImageUrl,
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
            var user = _authenticationService.GetCurrentUser();

            var data = new AddNewsData
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                ShorText = viewModel.ShorText,
                Text = viewModel.Text,
                CreatedDate = DateTime.Now,
                CategoryId = viewModel.SelectedCategoryId,
                Author = new CommentAuthorData
                {
                    Id = user.Id,
                    AuthorName = user.Name
                }
            };

            var thisNewsId = _newsRepository.SaveNews(data);

            _newsImageService.UploadNewsImages(thisNewsId, viewModel.Images, data.CreatedDate);

            return data;
        }

        public PaginatorViewModel<NewsForAdminPageViewModel> GetAllNewsForPagginator(int page, int perPage)
        {
            var viewModel = _paginatorService.GetPaginatorViewModel(
                page,
                perPage,
                BuildAdminNewsViewModel,
                _newsRepository
                );

            return viewModel;
        }

        public PaginatorViewModel<NewsCardViewModel> GetNewsCardForPaginator(int page, int perPage)
        {
            var viewModel = _paginatorService.GetPaginatorViewModel(
                page,
                perPage,
                BuildNewsCardViewModel,
                _newsRepository
                );

            return viewModel;
        }

        public NewsForAdminPageViewModel BuildAdminNewsViewModel(News dbNews)
        {
            return new NewsForAdminPageViewModel()
            {
                Id = dbNews.Id,
                Creator = new UserInfoViewModel()
                {
                    Id = dbNews.Creator.Id,
                    Name = dbNews.Creator.Name
                },
                TimeOfCreation = dbNews.CreatedTime,
                Title = dbNews.Title
            };
        }

        public NewsCardViewModel BuildNewsCardViewModel(News model)
        {
            var viewModel = new NewsCardViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                ShortText = model.ShorText,
                CreatedTime = model.CreatedTime,
                Author = model.Creator.Name,
                Category = model.Category.Name,
                PreviewImage = model.NewsImages.First().Url
            };

            return viewModel;
        }
    }
}
