using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class CheckoutDto
    {
        public ProductVM Product { get; set; }
        public CheckoutVM Checkout { get; set; }
    }
}
