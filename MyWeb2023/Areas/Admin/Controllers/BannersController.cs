using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class BannersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BannersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var banner = await _context.Banners.ToListAsync();
            var result = banner.Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DisplayLink = x.DisplayLink,
                Link = x.Link,
                Image = x.Image,
                IsActive = x.IsActive,
                Position = x.Position,
            }).ToList();
            return View(result);
        }
        //Create
        public IActionResult Create()
        {
            var listBanners = _context.Banners.Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DisplayLink = x.DisplayLink,
                Link = x.Link,
                Image = x.Image,
                IsActive = x.IsActive,
                Position = x.Position,
            }).ToList();
            ViewBag.Roles = listBanners;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBannerModel model)
        {
            var addBanner = new Banner
            {
                Title = model.Title,
                Description = model.Description,
                DisplayLink = model.DisplayLink,
                Link = model.Link,
                IsActive = model.IsActive,
                Position = model.Position,
            };
            _context.Banners.Add(addBanner);
            _context.SaveChanges();

            if (model.File != null)
            {
                string image = GetImage(addBanner.Id, model.File);
                addBanner.Image = image;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //Delete
        [HttpPost]
        public async Task<bool> DeleteBanners(int id)
        {
            var banner = _context.Banners.Find(id);
            _context.Banners.Remove(banner);
            _context.SaveChanges();
            return true;
        }
        //Update 
        [HttpGet]
        public IActionResult Update(int id)
        {
            var banner = _context.Banners.Find(id);
            return View(banner);
        }
        [HttpPost]
        public IActionResult Update(int id, string title, string description, string displaylink,
            string link, string image, bool isactive, int position)
        {
            var banner = _context.Banners.Find(id);
            banner.Update(title, description, displaylink, link, image, isactive, position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Up anh
        public string GetImage(int bannerId, IFormFile file)
        {
            var rootFolder = Directory.GetCurrentDirectory();
            string pathbanner = @$"{rootFolder}\wwwroot\data\banner\{bannerId}";

            if (!Directory.Exists(pathbanner))
            {
                Directory.CreateDirectory(pathbanner);
            }
            string filename = file.FileName;
            var filepath = Path.Combine(pathbanner, filename);

            using (FileStream filestream = System.IO.File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
            return filename;
        }
    }
}
