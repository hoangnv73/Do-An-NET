using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;
using System.Collections.Generic;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class CouponController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouponController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var coupon = await _context.Coupons.ToListAsync();
            var result = coupon.Select(x => new Coupon
            {
                Id = x.Id,
                CouponCode = x.CouponCode,
                Expired = x.Expired,
                Quantity = x.Quantity,
                TypeId = x.TypeId,
                Value = x.Value
            }).ToList();
            return View(result);
        }
        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CouponDto model)
        {
            var addCoupon = new Coupon
            {
                CouponCode = model.CouponCode,
                Expired = model.Expired,
                Quantity = model.Quantity,
                Value = model.Value,
                TypeId = model.TypeId
            };
            _context.Coupons.Add(addCoupon);
            _context.SaveChanges();
            return View();
        }
        //Delete
        public async Task<bool> DeleteCoupon(int id)
        {
            var coupon = _context.Coupons.Find(id);
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
            return true;
        }
        //Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var coupon = _context.Coupons.Find(id);
            if (coupon == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(coupon);
        }

        [HttpPost]
        public IActionResult Update(int id, string couponCode,double value, int typeId, int quantity, DateTime expired)
        {
            var coupon = _context.Coupons.Find(id);
            if (coupon == null)
            {
                ViewBag.Message = "Coupon khong ton tai";
                return View();
            }
            coupon.Update(couponCode, value, typeId, quantity, expired);
            _context.SaveChanges();
            return RedirectToAction("Update", new { id });

        }
    }
}
