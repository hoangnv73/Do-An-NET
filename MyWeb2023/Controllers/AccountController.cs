using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using MyWeb2023.Models;

namespace MyWeb2023.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            var userId = int.Parse(User.Identity.Name);
            var profile = _context.Users.Find(userId);
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
        public IActionResult Profile(int id, string firstname, string lastname)
        {
            var profile = _context.Users.Find(id);
            profile.Update(firstname, lastname);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { id = id });     
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile", "Account");
            }
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var userCookie = _httpContextAccessor.HttpContext.Request.Cookies["user"];
            if (userCookie != null)
            {
                Response.Cookies.Delete("user");
            }
           
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public object Login(string email, string password)
        {
            // Kiểm tra email có tồn tại hay không
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            // Nếu không tồn tại thì show lỗi
            if (user == null)
            {
                return new
                {
                    code = HttpStatusCode.BadRequest,
                    message = "Account does not exist"
                };
            }
            password = CommonFunction.HashPassword(password);
            
            if (user.CheckLogin >= 3 && (user.LastLogin?.AddMinutes(5) >= DateTime.Now))
            {
                return new
                {
                    code = HttpStatusCode.BadRequest,
                    message = "Account Locked"
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


            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
            };
            if (user.RoleId != null)
            {
                var roleName = _context.Roles.Find(user.RoleId)?.Name;
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }
            HttpContext.Response.Cookies.Append("user", user.Id.ToString());
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

             HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true });
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
                    Password = CommonFunction.HashPassword(password),
                    Email = email,
                    Gender = null,
                    LastLogin = DateTime.Now,
                };
                _context.Users.Add(UserAdd);
                _context.SaveChanges();
                SendMail("SignUp.html", email);
            }
            
            else
            {
                // Nếu not null kh cho tạo và show "TK đã tồn tại"
                ViewBag.Message = "Tài khoản đã tồn tại";
                return View();
            }
            return View();
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new string(stringChars);
            var user = _context.Users.FirstOrDefault(x =>  x.Email == email);
            if (user == null)
            {
                ViewBag.Message = "Email not found";
                return View();
            }
            //todo : send mail
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
