using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Note { get; set; }

        public List<OrderProductDto> Products { get; set; }

    }
}
