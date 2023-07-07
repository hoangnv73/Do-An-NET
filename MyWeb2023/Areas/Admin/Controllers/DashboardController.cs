using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myweb.Domain.Common;
using MyWeb.Infrastructure.Admin;
using MyWeb2023.Areas.Admin.Models;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.Where(x => x.Status == ORDER_STATUS.Delivered.Id);
            var totalUsers = _context.Users.Count();
            var totalRevenu = _context.OrderDetails.Sum(x => x.Price * x.Quantity);
            var dashboard = new DashboardDto()
            {
                TotalOrders = orders.Count(),
                TotalRevenu = totalRevenu.ToString("N2"),
                TotalUsers = totalUsers
            };
            return View(dashboard);
        }
    }
}
