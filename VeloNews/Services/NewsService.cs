﻿using Data.Interface.DataModels;
using Data.Interface.DataModels.NewsDataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using VeloNews.Models.NewsViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Services
{
    public class NewsService : INewsService
    {
        private IUserService _userService;
        private INewsRepository _newsRepository;
        private IImageRepository _imageRepository;
        private INewsCommentRepository _newsCommentRepository;
        private INewsCategoryRepository _newsCategoryRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public NewsService(INewsRepository newsRepository,
            IImageRepository imageRepository,
            INewsCommentRepository newsCommentRepository,
            IUserService userService,
            INewsCategoryRepository newsCategoryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _newsRepository = newsRepository;
            _imageRepository = imageRepository;
            _newsCommentRepository = newsCommentRepository;
            _userService = userService;
            _newsCategoryRepository = newsCategoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void EditNews(int id, string title, string text, string shorText)
        {
            _newsRepository.EditNews(id, title, text, shorText);
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
                    Author = x.Author.Name,
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
            var user = _userService.GetCurrentUser();

            var data = new AddNewsData
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                ShorText = viewModel.ShorText,
                Text = viewModel.Text,
                CreatedDate = DateTime.Now,
                CategoryId = viewModel.SelectedCategoryId,
                Author = new CreatorData
                {
                    Id = user.Id,
                    Name = user.Name
                }
            };

            var thisNews = _newsRepository.SaveNews(data);

            if (viewModel.Images == null)
            {
                var imageData = new ImageData
                {
                    Name = "defaultImage",
                    Url = $"/images/defaultNewsPreviewImage.jpg",
                    NewsId = thisNews
                };

                _imageRepository.SaveNewsImages(imageData);
            }
            else
            {
                var imageIndex = 1;

                foreach (var file in viewModel.Images)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var folderName = $"post{data.CreatedDate.ToString("ddMMyyyy")}";
                    var path = Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        "images",
                        "uploads",
                        "news",
                        folderName
                        );

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fileName = $"{thisNews}-{imageIndex}{extention}";

                    var fileNameWithPath = Path.Combine(path, fileName);

                    using (var fs = new FileStream(fileNameWithPath, FileMode.CreateNew))
                    {
                        file.CopyTo(fs);
                    }

                    var imageData = new ImageData
                    {
                        Name = fileName,
                        Url = $"/images/uploads/news/{folderName}/{fileName}",
                        NewsId = thisNews
                    };

                    _imageRepository.SaveNewsImages(imageData);
                    imageIndex++;
                }
            }

            return data;
        }
    }
}
