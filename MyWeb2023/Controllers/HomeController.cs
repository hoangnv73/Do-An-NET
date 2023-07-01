using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Models;
using MyWeb2023.Models.Dto;
using System.Diagnostics;

namespace MyWeb2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> _Banner()
        {
            var banners = await _context.Banners.ToListAsync();
            var result = banners.Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DisplayLink = x.DisplayLink,
                Link = x.Link,
                Image = x.Image,
                IsActive = x.IsActive,
                Position = x.Position,
            }).ToList();
            return View(result);
        }

        //// Product
        //public async Task<IActionResult> _Products()
        //{
        //    // Status = true thì hiển thị product
        //    var products = await _context.Products.Where(x => x.Status == true).ToListAsync();
        //    var result = products.Select(x => new ProductDto
        //    {
        //        Id = x.Id,
        //        Image = !string.IsNullOrEmpty(x.Image)
        //                ? $"/data/products/{x.Id}/{x.Image}"
        //                : "/www/images/default-thumbnail.jpg",
        //        Name = x.Name,
        //        BrandId = x.Id,
        //        Price = x.Price,
        //        Discount = x.Discount,
        //        Status = x.Status,
        //    }).ToList();
        //    return View(result);
        //}
    }
}