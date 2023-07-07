﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(CheckoutRequest request)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            }
            var order = new Order(userId, request.Address, request.Phone, request.Note, 
                request.CustomerName, request.Payment);
            _context.Orders.Add(order);
            _context.SaveChanges();

            double totalPrice = 0;
            foreach (var item in request.CartItems)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id ==item.ProductId && !x.IsDeleted);
                if (product == null) continue;
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Price = (product.Price - (product.Price / 100 * product.Discount ?? 0))
                    * (request.CartItems.FirstOrDefault(r => r.ProductId == product.Id)?.Quantity ?? 0),
                    Quantity = item.Quantity,
                };
                totalPrice = totalPrice + orderDetail.Price;
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();

            var resApi = "";
            if (request.Payment == "vnpay")
            {
                resApi = btnPay_Click(totalPrice * 23000);
            }
            else
            {
                resApi = "cod";
            }

            SendMail("SendMailOrder.html", request.Email);
            return resApi;
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
            var from_email = new EmailAddress("nguyenvanhoang73qb@gmail.com", "Kumo Shop");
            var subject = "Sending with Kumo Shop";
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


        protected string btnPay_Click(double amount)
        {
            //Get Config Info
            string vnp_Returnurl = "https://localhost:7032/Account/MyOrder"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "ADH2MKPG"; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = "XIEWSZDVZMTOMCLXMYXLFUFEAKPFQZKP"; //Secret Key
            //Get payment input
            OrderInfo order = new OrderInfo();
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = long.Parse(amount.ToString()); // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
            order.CreatedDate = DateTime.Now;
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());

            //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            //if (bankcode_Vnpayqr.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            //}
            //else if (bankcode_Vnbank.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            //}
            //else if (bankcode_Intcard.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            //}
            //dùng kiểu thanh toán ngân hàng nội địa
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            //vnpay.AddRequestData("vnp_BankCode", "INTCARD");

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());

            //if (locale_Vn.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_Locale", "vn");
            //}
            //else if (locale_En.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_Locale", "en");
            //}
            //chỉ dùng vn
            vnpay.AddRequestData("vnp_Locale", "vn");

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            //Response.Redirect(paymentUrl);
            return paymentUrl;
        }
    }
}
