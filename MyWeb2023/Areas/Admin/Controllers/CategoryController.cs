using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;
using MyWeb2023.Models;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/categories/{x.Id}/{x.Image}"
                        : "/admin/www/images/default.jpg",
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
            var AddCategory = new Category
            {
                Name = model.Name,
                IsActive = model.IsActive,
            };
            _context.Categories.Add(AddCategory);
            _context.SaveChanges();
            //-- nếu như ảnh not null thì cập nhật ảnh trong db & lưu ảnh trong folder 
            if (model.File != null)
            {
                string image = GetImage(AddCategory.Id, model.File);
                AddCategory.Image = image;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // Delete
        [HttpPost]
        public async Task<bool> DeleteCategory(int id)
        {
            var user = _context.Categories.Find(id);
            _context.Categories.Remove(user);
            _context.SaveChanges();
            return true;
        }

        //Update 
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(int id, string name, bool isActive, IFormFile? file)
        {
            var category = _context.Categories.Find(id);

            if (file != null)
            {
                var rootFolder = Directory.GetCurrentDirectory();
                var photoName = category.Image;
                string pathproduct = @$"{rootFolder}\wwwroot\data\categories\{id}\" + photoName;
                System.IO.File.Delete(pathproduct);
                category.Image = file.FileName;
                CommonFunction.UploadImage(file, $"categories/{category.Id}");
            }

            category.Update(name, isActive, category.Image);
            _context.SaveChanges(true);
            return RedirectToAction("Update", new { id });
        }

        public string GetImage(int categoryId, IFormFile file)
        {
            var rootFolder = Directory.GetCurrentDirectory();

            string pathcategory = @$"{rootFolder}\wwwroot\data\categories\{categoryId}";

            if (!Directory.Exists(pathcategory))
            {
                Directory.CreateDirectory(pathcategory);
            }
            string filename = file.FileName;
            var filepath = Path.Combine(pathcategory, filename);

            using (FileStream filestream = System.IO.File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
            return filename;
        }

    }
}
