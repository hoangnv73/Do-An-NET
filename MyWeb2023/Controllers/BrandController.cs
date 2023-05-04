using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // using EntityFrameworkCore
using Myweb.Domain.Models.Entities;
using MyWeb2023.Models;
using MyWeb2023.Models.Dto;

namespace MyWeb2023.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var brand = await _context.Brands.ToListAsync();

            var result = brand.Select(x => new BrandDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return View(result);
        }
        //Create
        public IActionResult Create() 
        {
        return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBrandModel model)
        {
            var addBrand = new Brand
            {
                Name = model.Name,
            };
            _context.Brands.Add(addBrand);
            _context.SaveChanges();
            return View();
        }

        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            _context.Brands.Remove(brand);
            _context.SaveChanges(true);
            return RedirectToAction("Index");
        }

        //Update
        [HttpGet]
        public ActionResult Update(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(brand);
        }
        [HttpPost]
        public ActionResult Update(int id, string name)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                ViewBag.Message = "San pham khong ton tai";
                return View();
            }
            brand.Update(name);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
