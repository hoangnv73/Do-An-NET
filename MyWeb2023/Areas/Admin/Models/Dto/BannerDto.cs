namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class BannerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DisplayLink { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
    }
}
