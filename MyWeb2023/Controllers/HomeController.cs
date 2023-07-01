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
            #region lưu lại để học
            //var home = new HomeDto();
            //var products = new List<ProductDto>();
            //products.Add(new ProductDto() { Id = 1, Name = "hoang" });
            //products.Add(new ProductDto() { Id = 2, Name = "van" });
            //products.Add(new ProductDto() { Id = 3, Name = "fvv" });
            //products.Add(new ProductDto() { Id = 4, Name = "dđ" });

            //var banners = new List<BannerDto>();
            //banners.Add(new BannerDto() { Id = 1, Title = "hioab" });
            //banners.Add(new BannerDto() { Id = 2, Title = "van" });
            //banners.Add(new BannerDto() { Id = 3, Title = "giodfd" });

            //home.Products = products;
            //home.Banners = banners;
            #endregion

            var home = new HomeDto();
            var products = new List<ProductDto>();
            products = _context.Products.Select(x => new ProductDto
            {
                Id = x.Id,
                Image = x.Image,
                Name = x.Name,
                Price = x.Price,
                Discount = x.Discount
            }).ToList();

            var banners = new List<BannerDto>();
			banners = _context.Banners.Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Link = x.Link,
                Image = x.Image
            }).ToList();

            home.Products = products;
            home.Banners = banners;

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

        //[HttpGet]
        //public async Task<IActionResult> _Banner()
        //{
        //    var banners = await _context.Banners.ToListAsync();
        //    var result = banners.Select(x => new BannerDto
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        Description = x.Description,
        //        DisplayLink = x.DisplayLink,
        //        Link = x.Link,
        //        Image = x.Image,
        //        IsActive = x.IsActive,
        //        Position = x.Position,
        //    }).ToList();
        //    return View(result);
        //}

    }
}