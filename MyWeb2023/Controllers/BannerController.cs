using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BannerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
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
