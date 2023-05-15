using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Models;
using MyWeb2023.Models.Dto;

namespace MyWeb2023.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //do data
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            var result = categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,

            }).ToList();
            return View(result);
        }
        // Create 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryModel model)
        {
            var AddCategory = new Category(model.Name, model.IsActive);
            _context.Categories.Add(AddCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges(true);
            return RedirectToAction("Index");
        }
        //Update 
        [HttpGet]
        public IActionResult Update(int id) 
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(int id, string name, bool isActive)
        {
            var category = _context.Categories.Find(id);
            category.Update(name, isActive);
            _context.SaveChanges(true);
            return RedirectToAction("Update", new {id});
        }
        
    }
}
