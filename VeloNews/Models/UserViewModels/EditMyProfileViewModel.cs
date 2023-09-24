using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VeloNews.Models.ValidationAttributes;

namespace VeloNews.Models.UserViewModels
{
    public class EditMyProfileViewModel
    {
        public int UserId { get; set; }
        [IsCorrectUserName]
        [IsUniqueUserName]
        public string Name { get; set; }
        public string Country { get; set; }
        public IFormFile? NewProfileImage { get; set; }
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        [IsCorrectDateOfBirthRange("01/01/1940", ErrorMessage ="Invalid date")]
        public DateTime DateOfBirth { get; set; }
    }
}
