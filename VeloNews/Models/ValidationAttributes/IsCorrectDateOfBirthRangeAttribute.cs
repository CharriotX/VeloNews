using Data.Interface.Models;
using System.ComponentModel.DataAnnotations;

namespace VeloNews.Models.ValidationAttributes
{
    public class IsCorrectDateOfBirthRangeAttribute : RangeAttribute
    {
        public IsCorrectDateOfBirthRangeAttribute(string minimum) : base(typeof(DateTime), minimum, DateTime.Now.ToShortDateString())
        {
        }
    }
}
