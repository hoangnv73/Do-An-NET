using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Order: EntityBase
    {
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
        public double Total { get; set; }

        public Order()
        {

        }
    }

}
