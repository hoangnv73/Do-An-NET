using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Repositories;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.RequestModel.Client;

namespace MyWeb2023.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CheckoutRequest request)
        {
            var order = new Order(null, request.Address, request.Phone, request.Note, request.CustomerName);
            _context.Orders.Add(order);
            _context.SaveChanges();
          
            foreach (var item in request.CartItems)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product == null) continue;
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Price = product.Price,
                    Quantity = item.Quantity,
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();
            return View();
        }
    }
}
