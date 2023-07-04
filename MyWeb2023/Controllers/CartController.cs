using Microsoft.AspNetCore.Mvc;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;

namespace MyWeb2023.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(List<CartItem> request)
        {
            var productIds = request.Select(x => x.ProductId).ToList();
            var products = _context.Products.Where(x => productIds.Contains(x.Id)).ToList();

            var response = new CartDto();
            response.Total = products.Sum(x => x.Price).ToString("N2");
            response.Items = products.Select(x => new ProductCartDto
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/products/{x.Id}/{x.Image}"
                        : "/data/default.png",
                Name = x.Name,
                Quantity = request.FirstOrDefault(r => r.ProductId == x.Id)?.Quantity ?? 0,
				Price = x.Price * (request.FirstOrDefault(r => r.ProductId == x.Id)?.Quantity ?? 0),
                //Price = x.Price - (x.Price/100 * x.Discount) 
            }).ToList();
            return PartialView("_Cart", response);
        }
    }
}
