using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Models;
using System.Collections.Generic;
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // Kiểm tra email có tồn tại hay không
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            password = HashPassword(password);
            // Nếu không tồn tại thì show lỗi
            if (user == null)
            {
                ViewBag.Message = "Tài khoản kh tồn tại";
                return View();
            }
            // Nếu tồn tại thì kiểm tra password 
            if (user.Password != password)
            {
                ViewBag.Message = "Mật khẩu không đúng";
                return View();
            }

            // Nếu mật khẩu đúng thì đăng nhập thành công
            return RedirectToAction("Index", "Home");


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
                };
                _context.Users.Add(UserAdd);
                _context.SaveChanges();
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
    }
}
