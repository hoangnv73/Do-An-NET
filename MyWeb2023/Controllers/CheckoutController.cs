using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
using MyWeb.Infrastructure.Repositories;
using MyWeb2023.Areas.Admin.Models;

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
            var checkout = await _context.Orders.ToListAsync();
            var result = checkout.Select(x => new CheckoutVM
            {
                Address = x.Address,
                Phone = x.Phone,
                Note = x.Note,
                OrderDate = x.OrderDate,
            }).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult Index(CheckoutVM model)
        {
            var order = new Order
            {
                Address = model.Address,
                Phone = model.Phone,
                Note = model.Note,
                OrderDate = DateTime.Now,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            int orderId = order.Id;
            var productIds = new List<int> { 55, 56, 57};
           
            foreach (var item in productIds)
            {
                var product = _context.Products.Find(item);
                var orderDetail = new OrderDetails
                {
                    OrderId = orderId,
                    ProductId = item,
                    Price = product.Price,
                };
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }

            
            return View();
        }
    }
}
