namespace VeloNews.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public IFormFile UploadImage { get; set; }
    }
}
