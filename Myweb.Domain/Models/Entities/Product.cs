using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Product: EntityBase
    {
        public string? Image { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? BrandId { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; } 
        public int? CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public Product()
        {

        }
        public Product(string name, double price, double? discount, bool status, string image, string description)
        {
            Name = name;
            Price = price;
            Discount = discount;
            Status = status;
            Image = image;
            Description = description;
            IsDeleted = false;
        }

        public void Update(string name, double price, double? discount, bool status,
            string? image, int brandId, string description, int? categoryId)
        {
            Name = name;
            Price = price;
            Discount = discount;
            Status = status;
            Image = image;
            BrandId = brandId;
            Description = description;
            CategoryId = categoryId;
        }
    }
}
