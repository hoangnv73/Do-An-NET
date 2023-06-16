using Microsoft.AspNetCore.Mvc;
using MyWeb2023.Areas.Admin.Models.Dto;
using MyWeb2023.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MyWeb2023.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var review = await _context.Reviews.ToListAsync();
            var result = review.Select(x => new ReviewDto
            {
                Id = x.Id,
                Name = x.Name,
                Rating = x.Rating,
                Comment = x.Comment,
                CreateDate = x.CreateDate,
            }).ToList();
            return View(result);
        }
    }
}
