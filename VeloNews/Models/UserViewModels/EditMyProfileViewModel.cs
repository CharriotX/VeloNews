﻿using Data.Interface.Models.enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class EditMyProfileViewModel
    {
        public int UserId { get; set; }

        [MinLength(4, ErrorMessage = "Minimum username length 4 characters")]
        [EditProfileUsername]
        public string Name { get; set; }
        public string Country { get; set; }
        public IFormFile? NewProfileImage { get; set; }
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        [IsCorrectDateOfBirthRange("01/01/1940", ErrorMessage ="Invalid date")]
        public DateTime DateOfBirth { get; set; }
        public UserLanguage Language { get; set; }
        public List<SelectListItem> Languages { get; set; } = new List<SelectListItem>();
    }
}
