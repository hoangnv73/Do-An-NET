﻿using MyWeb.Infrastructure.Client;

namespace MyWeb2023.Models.Dto
{
    public class HomeDto
    {
        public List<BannerDto> Banners { get; set; }
        public List<ProductVM> Products { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
