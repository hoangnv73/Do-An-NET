using Myweb.Domain.Common;
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
        public string Address { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string? Note { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public List<OrderProductDto> Products { get; set; }
        public List<COMMON_STATUS> OrderStatuses { get; set; } = new List<COMMON_STATUS> { };

    }
}
