using System.Globalization;

namespace VeloNews.Models
{
    public class CultureSwitcherViewModel
    {
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SuportedCultures { get; set; }
    }
}
