using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;
using MyWeb2023.Models;
using MyWeb2023.RequestModel.Admin;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class BannersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="context"></param>
        public BannersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET List banners
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners.OrderBy(x => x.Position).ToListAsync();
            var result = banners.Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/banners/{x.Id}/{x.Image}"
                        : "/www/images/default-thumbnail.jpg",
                IsActive = x.IsActive,
                Position = x.Position,
            }).ToList();
            return View(result);
        }

        /// <summary>
        /// Page create banner
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// CREATE banner
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CreateBannerModel request)
        {
            //-- Add banner
            var banner = new Banner(request.Title, request.Description, request.DisplayLink, request.Link, request.File.FileName, request.Position);
            _context.Banners.Add(banner);
            _context.SaveChanges();

            //-- Save Image
            CommonFunction.UploadImage(request.File, $"banners/{banner.Id}");
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Delete Banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteBanner(int id)
        {
            var banner = _context.Banners.Find(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                var rootFolder = Directory.GetCurrentDirectory();
                string pathproduct = @$"{rootFolder}\wwwroot\data\banners\{id}";
                if (Directory.Exists(pathproduct)) Directory.Delete(pathproduct, true);
                _context.SaveChanges();
            }
            return true;
        }
        
        
        [HttpGet]
        public IActionResult Update(int id)
        {
            var banner = _context.Banners.Find(id);
            if (banner == null) return RedirectToAction("NotFound", "Common");
            return View(banner);
        }

        [HttpPost]
        public IActionResult Update(int id, string title, string description, string displaylink,
            string link, IFormFile? file, bool isactive, int position)
        {
            var banner = _context.Banners.Find(id);
            if (banner == null) return RedirectToAction("NotFound", "Common");
            if (file != null)
            {
                banner.Image = file.FileName;
                CommonFunction.UploadImage(file, $"banners/{banner.Id}");
            }
            banner.Update(title, description, displaylink, link, banner.Image, isactive, position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
