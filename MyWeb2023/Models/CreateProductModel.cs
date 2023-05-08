namespace MyWeb2023.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Brand { get; set; } = string.Empty;
        public double? Discount { get; set; }
        public bool Status { get; set; }
        public IFormFile? File { get; set; }
        public int BrandId { get; set; }
        
    }
}
