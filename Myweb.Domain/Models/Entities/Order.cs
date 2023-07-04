using Myweb.Domain.Common;
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
        public int? UserId { get; set; }
        /// <summary>
        /// <see cref="ORDER_STATUS"/>
        /// </summary>
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; } 
        public int Phone { get; set; }
        public string Note { get; set; }
        public string CustomerName { get; set; }

        public Order()
        {

        }

        public void Update(int status)
        {
            Status = status;
        }
    }

}
