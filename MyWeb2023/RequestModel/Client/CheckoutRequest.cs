using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb2023.RequestModel.Client
{
    public class CheckoutRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string? Note { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        /// <summary>
        /// cod, paypal
        /// </summary>
        public string Payment { get; set; } = "cod";
        public List<ProductModel> Products { get; set; } = new List<ProductModel> { };
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
