using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb.Infrastructure.Client;
using MyWeb2023.Areas.Admin.Models;
using PagedList;

namespace MyWeb2023.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index(string sort, int? page, int? categoryId,int id)
        {
            ViewBag.Page = page == null ? 1 : page;

            // Status = true thì hiển thị product
            var products = await _context.Products.Where(x => x.Status && !x.IsDeleted).Take(12).ToListAsync();
            if(categoryId != null)
            {
                products = products.Where(x => x.CategoryId == categoryId).ToList();
            }
            if (sort == "price_asc")
            {
                products = products.OrderBy(x => x.Price - ((x.Price / 100) * x.Discount)).ToList();
            }
            if (sort == "price_desc")
            {
                products = products.OrderByDescending(x => x.Price - ((x.Price / 100) * x.Discount)).ToList();
            }

            var toralReview = _context.Reviews.Count(x => x.ProductId == id) ;  
           
            var result = products.Select(x => new ProductVM
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/products/{x.Id}/{x.Image}"
                        : "/data/default.png",
                Name = x.Name,
                BrandId = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                Status = x.Status,
                TotalReview = toralReview,
              
            }).ToList();
            return View(result); 
        }

        // Show BrandName
        public string ShowBrandName(int? brandId)
        {
            if (brandId == null)
            {
                return "No Brand";
            }
            else
            {
                var brand = _context.Brands.Find(brandId);
                string brandName = brand.Name;
                return brandName;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var response = new ProductDetailsDto();
            var totalReview = _context.Reviews.Count(x => x.ProductId == id);
            var sumReview = _context.Reviews.Where(x => x.ProductId == id).Sum(x => x.Rating);  
            var tbcReview = sumReview / totalReview;

            var product = _context.Products.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Status = product.Status,
                Discount = product.Discount,
                Image = product.Image,
                BrandName = ShowBrandName(product.BrandId),
                TotalReview = totalReview,
                TBCReview = tbcReview
            };

            var listReviews = await _context.Reviews.Where(x => x.ProductId == id).ToListAsync();
            var reviews = listReviews.Select(x => new ReviewDto
            {
                Comment = x.Comment,
                Id = x.Id,
                Name = x.Name,
                CreateDate = x.CreateDate,
                Rating = x.Rating
            }).ToList();

            response.Product = productVM;
            response.Reviews = reviews;
            return View(response);
        }

        [HttpPost]
        public IActionResult Comment( int rating, string comment, int productId)
        {
            var userId = int.Parse(User.Identity.Name);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var addReview = new Review
            {
                UserId = userId,
                Rating = rating,
                Comment = comment,
                CreateDate = DateTime.Now,
                ProductId = productId,
            };
            _context.Reviews.Add(addReview);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = productId });
        }






    }
}
