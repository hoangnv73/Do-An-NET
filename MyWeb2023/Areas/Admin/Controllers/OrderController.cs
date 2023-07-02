using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
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
            return RedirectToAction("Order", "Admin");
        }

        /// <summary>
        /// Order Details
        /// </summary>
        /// <param name="id">OrderId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Details(int id) 
        {
            var response = new OrderDetailsDto();
            var order = _context.Orders.Find(id);

            response.Id = order.Id;
            response.OrderDate = order.OrderDate;
            response.Address = order.Address;
            response.Phone = order.Phone;
            response.Note = order.Note;
            response.Status = order.Status;

            var orderDetails = _context.OrderDetails.Where(x => x.OrderId == id).ToList();
            var myArr = new List<OrderProductDto>();

            foreach (var item in orderDetails)
            {
                var product = _context.Products.Find(item.ProductId);
                var orderProduct = new OrderProductDto()
                {
                    ProductId = item.ProductId,
                    Name = product.Name,
                    Price = product.Price
                };
            }
            response.Products = myArr;
            return View(response);

        }
    }
}
