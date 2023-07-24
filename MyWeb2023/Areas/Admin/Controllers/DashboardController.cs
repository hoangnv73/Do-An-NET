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
            var totalProducts = _context.Products.Count();
            var totalRevenu = _context.OrderDetails.Sum(x => x.Price * x.Quantity);
            var revenuNow = 100;

            var ordersMonthNow = _context.Orders.Where(x => x.OrderDate.AddMonths(-1).Month == DateTime.Now.AddMonths(-1).Month).Count();
            var ordersMonth = _context.Orders.Where(x => x.OrderDate.Month == DateTime.Now.Month).Count();

            var dashboard = new DashboardDto()
            {
                TotalOrders = orders.Count(),
                TotalRevenu = Math.Round(totalRevenu, 2),
                TotalUsers = totalUsers,
                TotalProducts = totalProducts,
                GrowthRevenu = Math.Round(((revenuNow - totalRevenu)/totalRevenu) * 100, 2), 
              
            };
           
            return View(dashboard);
        }
    }
}
