namespace MyWeb2023.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public double Price { get; set; }

        public double? Discount { get; set; }

        public bool Status { get; set; }
    }
}
