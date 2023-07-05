using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class CartDto
    {
        public string Total { get; set; } = string.Empty;
        public List<ProductCartDto> Items { get; set; } = new List<ProductCartDto>();

    }
    public class ProductCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }

    }

    public class CartItem
    {
        public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
