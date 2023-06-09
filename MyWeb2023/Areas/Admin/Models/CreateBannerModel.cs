namespace MyWeb2023.Areas.Admin.Models
{
    public class CreateBannerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DisplayLink { get; set; }
        public string Link { get; set; }
        public IFormFile? File { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
    }
}

