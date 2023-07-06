using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Repositories;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.RequestModel.Client;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace MyWeb2023.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CheckoutRequest request, string email)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            }
            var order = new Order(userId, request.Address, request.Phone, request.Note, request.CustomerName);
            _context.Orders.Add(order);
            _context.SaveChanges();
          
            foreach (var item in request.CartItems)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id ==item.ProductId && !x.IsDeleted);
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
            SendMail("SendMailOrder.html", email);
            return View();
        }

        public List<ObjectMail> GetObjectMails()
        {
            var res = new List<ObjectMail>();
            res.Add(new ObjectMail() { Key = "{FirstName}", Value = "Kumo" });
            res.Add(new ObjectMail() { Key = "{DateTime.Now}", Value = DateTime.Now.ToString() });
            return res;
        }
        public async Task SendMail(string key, string mailTo)
        {
            var objMails = GetObjectMails();
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from_email = new EmailAddress("nguyenvanhoang73qb@gmail.com", "Mamz Shop");
            var subject = "Sending with Mamz Shop";
            var to_email = new EmailAddress("maxgamingtvchannel@gmail.com", "Example User");
            var plainTextContent = "";
            var rootFolder = Directory.GetCurrentDirectory();
            string path = @$"{rootFolder}\wwwroot\template\{key}";
            var htmlContent = System.IO.File.ReadAllText(path);
            foreach (var item in objMails)
            {
                htmlContent = htmlContent.Replace(item.Key, item.Value);
            }
            var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
