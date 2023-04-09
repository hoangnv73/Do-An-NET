using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Product: EntityBase
    {
        public string? Image { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty; 
        public double Price { get; set; }
        public double? Discount { get; set; }
        public int Quantity { get; set; }

        public Product()
        {

        }
        public Product(string name, double price, double? discount, int soLuong)
        {
            Name = name;
            Price = price;
            Discount= discount;
            Quantity = soLuong;

        }

        public void Update(string name, double price, double? discount, int quantity)
        {
            Name = name;
            Price = price;
            Discount = discount;
            Quantity = quantity;
        }
    }
}
