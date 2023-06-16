using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
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
        [HttpPost]
        public async Task<bool> DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return true;
        }
    }
}
