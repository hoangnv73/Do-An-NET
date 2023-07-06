﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myweb.Domain.Models.Entities
{
    public class OrderDetail : EntityBase 
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}