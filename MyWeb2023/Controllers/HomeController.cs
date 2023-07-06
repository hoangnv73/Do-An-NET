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
            var home = new HomeDto();
            var products = new List<ProductVM>();
            products = _context.Products.Where(x => x.Status && !x.IsDeleted).Select(x => new ProductVM
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/products/{x.Id}/{x.Image}"
                        : "/www/images/default-thumbnail.jpg",
                Name = x.Name,
                Price = x.Price,
                Discount = x.Discount
            }).Take(12).ToList();

            var banners = new List<BannerDto>();
			banners = _context.Banners.Where(x => x.IsActive).Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Link = x.Link,
                Image = x.Image
            }).ToList();

            var categories = new List<CategoryDto>();
            categories = _context.Categories.Where(x => x.IsActive).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                IsActive = x.IsActive
            }).ToList();


            home.Products = products;
            home.Banners = banners;
            home.Categories = categories;
            return View(home);
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

        

    }
}