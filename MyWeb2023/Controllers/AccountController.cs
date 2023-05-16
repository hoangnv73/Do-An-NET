using Microsoft.AspNetCore.Mvc;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Models;
using System.Collections.Generic;

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
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                ViewBag.Message = "Tài khoản kh tồn tại";
                return View();
            }
            if (user.Password != password)
            {
                ViewBag.Message = "Mật khẩu không đúng";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string email, string password)
        {
          
            return View();
        }
    }
}
