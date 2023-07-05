namespace MyWeb2023.RequestModel.Admin
{
    public class CreateBannerModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DisplayLink { get; set; }
        public string? Link { get; set; }
        public IFormFile File { get; set; }
        public int Position { get; set; }
    }
}

