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

        public IActionResult Index(List<int> productIds)
        {
            
            var products = _context.Products.Where(x => productIds.Contains(x.Id)).ToList();
            return View();
        }
    }
}
