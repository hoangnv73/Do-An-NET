using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class CartDto
    {
        public int Total { get; set; }
        public List<ProductCartDto> ProductCartDtos { get; set; }

    }
    public class ProductCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

    }
}
