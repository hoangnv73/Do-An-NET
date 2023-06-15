using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;

namespace MyWeb2023.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string sort, int? page)
        {
            ViewBag.Page = page == null ? 1 : page;
            var products = await _context.Products.ToListAsync();

            if (sort == "price_asc")
            {
                products = products.OrderBy(x => x.Price).ToList();
            }
            if (sort == "price_desc")
            {
                products = products.OrderByDescending(x => x.Price).ToList();
            }
            var result = products.Select(x => new ProductVM
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/products/{x.Id}/{x.Image}"
                        : "/www/images/default-thumbnail.jpg",
                Name = x.Name,
                BrandId = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                Status = x.Status,
            }).ToList();
            return View(result);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(product);
        }


        // Reviews
        //      public async Task<IActionResult> Details()
        //{
        //          var review = await _context.Reviews.ToListAsync();
        //	var result = review.Select(x => new ReviewDto
        //	{
        //		Id = x.Id,
        //		Name = x.Name,
        //		Rating = x.Rating,
        //		Comment = x.Comment,
        //		PostDate = x.PostDate,
        //	}).ToList();
        //	return View(result);
        //}
        public async Task<IActionResult> Details()
        {
            //--- response
            var response = new ProductDetailsDto();
            //-- Get Reviews
            var listReviews = await _context.Reviews.ToListAsync();
            var reviews = listReviews.Select(x => new ReviewDto
            {
                Comment = x.Comment,
                Id = x.Id,
                Name = x.Name,
                PostDate = x.PostDate,
                Rating = x.Rating
            }).ToList();

            //var reviews = new List<ReviewDto>();

            //foreach (var item in listReviews)
            //{
            //    var reviewItem = new ReviewDto
            //    {
            //        Id = item.Id,
            //        Comment = item.Comment,
            //    };
            //    reviews.Add(reviewI0tem);
            //}



            //--Get product

            return View(response);
        }

        [HttpPost]
        public IActionResult Comment(string name, int rating, string comment)
        {
            var addReview = new Review
            {
                Name = name,
                Rating = rating,
                Comment = comment,
                PostDate = DateTime.Now,
            };
            _context.Reviews.Add(addReview);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }


    }
}
