using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Interface.Models
{
    public class Image : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual News News { get; set; }
    }
}
