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
            var result = checkout.Select(x => new CheckoutDto
            {
                Address = x.Address,
                Phone = x.Phone,
                Note = x.Note,
                OrderDate = x.OrderDate,
            }).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult Index(CheckoutDto model)
        {
            var checkout = new Order
            {
                Address = model.Address,
                Phone = model.Phone,
                Note = model.Note,
                OrderDate = DateTime.Now,
            };
            _context.Orders.Add(checkout);
            _context.SaveChanges();
            return View();
        }
    }
}
