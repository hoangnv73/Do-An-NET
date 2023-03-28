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

        public double Discount { get; set; }

        public int Quantity { get; set; }
    }
}
