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
                //var price = item.Price - (item.Price / 100) * item.Discount;
                products = products.OrderBy(x => x.Price-((x.Price/100)*x.Discount)).ToList();
            }
            if (sort == "price_desc")
            {
                products = products.OrderByDescending(x => x.Price - ((x.Price / 100) * x.Discount)).ToList();
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

        [HttpGet]
        // Đây là id truyền vào
        public async Task<IActionResult> Details(int id)
        {
            //--- response
            var response = new ProductDetailsDto();
            //-- Get Reviews 
            // where database ProductId = id truyền vào 
            var listReviews = await _context.Reviews.Where(x => x.ProductId == id).ToListAsync();
            var reviews = listReviews.Select(x => new ReviewDto
            {
                Comment = x.Comment,
                Id = x.Id,
                Name = x.Name,
                CreateDate = x.CreateDate,
                Rating = x.Rating,
                ProductId = x.ProductId,
            }).ToList();

            //--Get product
            var product = _context.Products.Find(id);
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Status = product.Status,
                Discount = product.Discount,
            };

            response.Reviews = reviews;
            response.Product = productVM;

            return View(response);
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
        }

        [HttpPost]
        public IActionResult Comment(string name, int rating, string comment)
        {
            var addReview = new Review
            {
                Name = name,
                Rating = rating,
                Comment = comment,
                CreateDate = DateTime.Now,
            };
            _context.Reviews.Add(addReview);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }


    }
}
