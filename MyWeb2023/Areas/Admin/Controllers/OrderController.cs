using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
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
                Status = x.Status,
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
        [HttpPost]
        public IActionResult Create(OrderDto model)
        {
            var addOrder = new Order
            {
                OrderDate = model.OrderDate,
                Address = model.Address,
                Phone = model.Phone,
                Note = model.Note,
                Status = model.Status,
            }; 

            return View();
        }

        //Update
        [HttpGet]
        public ActionResult Update(int id)
        {
            var order = _context.Orders.Find(id);
            return View(order);
        }
        [HttpPost]
        public ActionResult Update(int id, int status)
        {
            var order = _context.Orders.Find(id);

            order.Update(status);
            _context.SaveChanges();
            return RedirectToAction("Index", "Order");
        }
    }
}
