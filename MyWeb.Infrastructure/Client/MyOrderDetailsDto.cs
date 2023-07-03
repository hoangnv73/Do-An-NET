using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class MyOrderDetailsDto
    {
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } 
        public int Quantity { get; set; }
    }
}
