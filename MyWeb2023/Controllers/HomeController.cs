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
    }
}