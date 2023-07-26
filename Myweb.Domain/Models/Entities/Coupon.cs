using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Myweb.Domain.Models.Entities
{
    public class Coupon : EntityBase
    {
        public string CouponCode { get; set; }
        public double Value { get; set; }
        public int TypeId { get; set; }
        public int Quantity { get; set; }
        public DateTime Expired { get; set; }

        public void Update(string couponCode,double value, int typeId, int quantity, DateTime expired) 
        { 
            CouponCode = couponCode;
            Value = value;
            TypeId = typeId;
            Quantity = quantity;
            Expired = expired;
        }
        public Coupon()
        {

        }
    }
}
