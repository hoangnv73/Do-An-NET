﻿namespace MyWeb2023.Areas.Admin.Models.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime PostDate { get; set; }
    }
}