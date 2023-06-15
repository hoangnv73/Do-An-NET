using MyWeb2023.Areas.Admin.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.Infrastructure.Client
{
    public class ProductDetailsDto
    {
      
        public ProductVM Product { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
