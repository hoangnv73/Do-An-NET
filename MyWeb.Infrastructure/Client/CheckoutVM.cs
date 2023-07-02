using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class CheckoutVM
    {
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; } 
        public int Phone { get; set; }
        public string Note { get; set; }

        //public List<int> ProductIds { get; set; }
        //public List<ProductModel> Products { get; set; }
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
