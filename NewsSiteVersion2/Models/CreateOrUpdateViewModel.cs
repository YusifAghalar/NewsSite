using Microsoft.AspNetCore.Http;
using NewsEntities.Concrete;


namespace NewsSiteVersion2.Models
{
    public class CreateOrUpdateViewModel
    {
        public IFormFile image { get; set; }
        public News news { get; set; }
        public string imagePath { get; set; }
    }
}
