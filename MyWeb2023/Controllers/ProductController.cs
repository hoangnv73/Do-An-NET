using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Controllers
{
    public class ProductController : Controller
    {
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index(string sort, int? page)
		{
			ViewBag.Page = page == null ? 1 : page;
			var products = await _context.Products.ToListAsync();
			

            var result = products.Select(x => new ProductVM
            {
				Id = x.Id,
				Image = !string.IsNullOrEmpty(x.Image)
						? $"/data/{x.Id}/{x.Image}"
						: "/www/images/default-thumbnail.jpg",
				Name = x.Name,
				BrandId = x.Id,
				Price = x.Price,
				Discount = x.Discount,
				Status = x.Status,
			}).ToList();
			return View(result);
		}
		

        public IActionResult Details()
        {
            return View();
        }
    }
}
