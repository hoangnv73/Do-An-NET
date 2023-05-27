namespace MyWeb2023.Areas.Admin.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double? Discount { get; set; }
        public bool Status { get; set; }
        public IFormFile? File { get; set; }
        public int? BrandId { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
