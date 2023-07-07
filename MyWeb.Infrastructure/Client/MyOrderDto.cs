using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class MyOrderDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public List<MyOrderDetailsDto> Details { get; set; }

    }
}
