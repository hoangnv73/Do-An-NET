using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb2023.Models;

namespace MyWeb2023.Controllers
{
    public class CakeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CakeController(ApplicationDbContext myWorldDbContext)
        {
            _context = myWorldDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var cakes = await _context.Cakes.ToListAsync();
          
            return View();
        }
    }
}
