namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int BrandId { get; set; }

        public double Price { get; set; }

        public double? Discount { get; set; }

        public bool Status { get; set; }

        public string BrandName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

      
    }
    public class FilterProduct
    {
        public string Sort { get; set; }
        public string Kw { get; set; }
        public string Brand { get; set; }
    }

}
