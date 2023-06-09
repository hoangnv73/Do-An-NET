using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Areas.Admin.Controllers
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
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/{x.Id}/{x.Image}"
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
        public IActionResult Update(int id, string name, bool isActive)
        {
            var category = _context.Categories.Find(id);
            category.Update(name, isActive);
            _context.SaveChanges(true);
            return RedirectToAction("Update", new { id });
        }


        public string GetImage(int categoryId, IFormFile file)
        {
            //// Get the current directory.
            var rootFolder = Directory.GetCurrentDirectory();
            //-- khai báo đường dẫn
            string pathcategory = @$"{rootFolder}\wwwroot\admin\data\{categoryId}";

            //-- Kiểm tra folder đã tồn tại hay chưa
            if (!Directory.Exists(pathcategory))
            {
                //-- Nếu chưa có thì tạo mới
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
