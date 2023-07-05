using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;
using System.Data;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var catalog = await _context.Catalogs.ToListAsync();
            var result = catalog.Select(x => new Catalog
            {
                Id = x.Id,
                Title = x.Title,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/catalog/{x.Id}/{x.Image}"
                        : "/admin/www/images/default.jpg",
                Description = x.Description,
            }).ToList();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Catalog catalog)
        {
            var addCatalog = new Catalog
            {
                Id = catalog.Id,
                Title = catalog.Title,
                Image = catalog.Image,
                Description = catalog.Description,
            };
            _context.Catalogs.Add(addCatalog);
            _context.SaveChanges();
            return View();
        }
        [HttpPost]
        public async Task<bool> DeleteCatalog(int id)
        {
            var catalog = _context.Catalogs.Find(id);
            _context.Catalogs.Remove(catalog);
            _context.SaveChanges();
            return true;
        }
    }
}
