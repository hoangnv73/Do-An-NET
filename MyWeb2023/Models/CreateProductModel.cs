namespace MyWeb2023.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Brand { get; set; } = string.Empty;
        public double? Discount { get; set; }
        public int Quantity { get; set; }
    }
}
