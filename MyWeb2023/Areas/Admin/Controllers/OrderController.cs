using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();
            var result = orders.Select(x => new OrderDto
            {
                Id = x.Id,
                Code = x.Code,
                Status = x.Status,
                Total = x.Total
            }).ToList();
            return View(result);
        }
    }
}
