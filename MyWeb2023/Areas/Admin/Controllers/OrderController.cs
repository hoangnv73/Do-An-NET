using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Common;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;
using System.Reflection;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.OrderByDescending(x => x.OrderDate).ToListAsync();
            var result = orders.Select(x => new OrderDto
            {
                Id = x.Id,
                StatusName = ORDER_STATUS.GetDetail(x.Status)?.Name ?? "-",
                OrderDate = x.OrderDate,
                Address = x.Address,
                Phone = x.Phone,    
                Note = x.Note,
            }).ToList();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Update
        [HttpGet]
        public ActionResult Update(int id)
        {
			var order = _context.Orders.Find(id);
			if (order == null) return RedirectToAction("NotFound", "Common");
			
			


			var orderDetails = _context.OrderDetails.Where(x => x.OrderId == id).ToList();
			var products = new List<OrderProductDto>();

			foreach (var orderDetail in orderDetails)
			{
				var product = _context.Products.Find(orderDetail.ProductId);
                if (product == null) continue;
                var orderProduct = new OrderProductDto()
                {
                    ProductId = orderDetail.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = orderDetail.Quantity,
                    Image = !string.IsNullOrEmpty(product.Image)
                        ? $"/data/products/{product.Id}/{product.Image}"
                        : "/data/default.png",
                    Status = product.IsDeleted ? "Unavailable" : "Active"
                };
				products.Add(orderProduct);
			}
            var response = new OrderDetailsDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Address = order.Address,
                Phone = order.Phone,
                Note = order.Note,
                Status = order.Status,
                CustomerName = order.CustomerName,
                TotalPrice = products.Sum(x => x.Price * x.Quantity),
                Products = products,
                OrderStatuses = ORDER_STATUS.GetList(),
		    };
			return View(response);
		}
        [HttpPost]
        public ActionResult Update(int id, int status)
        {
            var order = _context.Orders.Find(id);

            order.Update(status);
            _context.SaveChanges();
            return RedirectToAction("Order", "Admin");
        }
    }
}
