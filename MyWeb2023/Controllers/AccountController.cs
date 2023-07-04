using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Myweb.Domain;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace MyWeb2023.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        //todo
        [HttpGet]
        public IActionResult Profile(int id)
        {
            //todo : hardcode
            id = 132;
            var profile = _context.Users.Find(id);
            var profileDto = new ProfileDto
            {
                Id = profile.Id,
                Email = profile.Email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
            };
            return View(profileDto);
        }

        [HttpPost]
        public IActionResult Profile(int id, string firstname, string lastname, string email)
        {
            var profile = _context.Users.Find(id);
            profile.Update(firstname, lastname, email);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = id });     
        }

        public IActionResult Login()
        {
            //SendMail();
            return View();
        }

        [HttpPost]
        public object Login(string email, string password)
        {
            // Kiểm tra email có tồn tại hay không
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

             password = HashPassword(password);
            
            if (user.CheckLogin >= 3 && (user.LastLogin?.AddMinutes(5) >= DateTime.Now))
            {
                return new
                {
                    code = HttpStatusCode.BadRequest,
                    message = "Account Locked"
                };
            }

            // Nếu không tồn tại thì show lỗi
            if (user == null)
            {
                return new
                {
                    code = HttpStatusCode.BadRequest,
                    message = "Account does not exist"
                };
            } 
            // Nếu tồn tại thì kiểm tra password 
            if (user.Password != password)
            {
                user.CheckLogin = user.CheckLogin + 1;
                user.LastLogin = DateTime.Now;
                _context.SaveChanges();
                return new
                {
                    code = 400,
                    message = "Wrong Password"
                };
            }

            var obj = new
            {
                code = HttpStatusCode.OK,
                roleId = user.RoleId
            };

            user.CheckLogin = 0;
            _context.SaveChanges();
            return obj;

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string email, string password, string firstName, string lastname)
        {
            var check = _context.Users.FirstOrDefault(x => x.Email == email);
            if (check == null)
            {
                //Nếu bằng null cho phép tạo TK
                var UserAdd = new User
                {
                    FirstName = firstName,
                    LastName = lastname,
                    Password = HashPassword(password),
                    Email = email,
                    Gender = null,
                    LastLogin = DateTime.Now,
                };
                _context.Users.Add(UserAdd);
                _context.SaveChanges();
                //SendMail();
            }
            
            else
            {
                // Nếu not null kh cho tạo và show "TK đã tồn tại"
                ViewBag.Message = "Tài khoản đã tồn tại";
                return View();
            }
            return View();
        }

        public string HashPassword(string password)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public IActionResult MyOrder(int userId) 
        {
            // khai bao doi tuong
            var response = new List<MyOrderDto>();
            var orders = _context.Orders.Where(x => x.UserId == userId).ToList();

            foreach (var order in orders)
            { 
                var orderDetails = _context.OrderDetails.Where(x => x.OrderId == order.Id).ToList();
                var details = new List<MyOrderDetailsDto>();
               
                foreach (var orderDetail in orderDetails)
                {
                    var product = _context.Products.Find(orderDetail.ProductId);
                    var aa = new MyOrderDetailsDto()
                    {
                        Image = !string.IsNullOrEmpty(product.Image)
                        ? $"/data/products/{product.Id}/{product.Image}"
                        : "/www/images/default-thumbnail.jpg",
                        Name = product.Name,
                        Price = orderDetail.Price
                    };
                    details.Add(aa);
                }
                // gan du lieu
                var myOrder = new MyOrderDto()
                {
                    Id = order.Id,
                    Address = order.Address,
                    Status = order.Status,
                    OrderDate = order.OrderDate,
                    Details = details
                };
                response.Add(myOrder);
            }

            return View(response);
        }

        public IActionResult ForgotPassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new string(stringChars);

            return View();
        }

        public List<ObjectMail> GetObjectMails()
        {
            //var di = new Dictionary<string, string>();
            //di.Add("firstName", "honga");
            //var k = new List<KeyValuePair<string, string>>();

            var res = new List<ObjectMail>();
            res.Add(new ObjectMail() { Key = "{FirstName}", Value = "Hoàng" });
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
            string path = @$"{rootFolder}\wwwroot\template\SignUp.html";
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
