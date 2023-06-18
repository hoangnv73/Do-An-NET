namespace MyWeb2023.Models.Dto
{
    public class BannerDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DisplayLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Position { get; set; }
    }
}
