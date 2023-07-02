using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Admin
{
    public class DashboardDto
    {
        public int TotalOrders { get; set; }
        public string TotalRevenu { get; set; }
        public int TotalUsers { get; set; }
    }
}
