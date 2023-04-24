using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Models;
using MyWeb2023.Models.Dto;

namespace MyWeb2023.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string sort)
         {
            var products = await _context.Products.ToListAsync();

            if (sort == "price_asc")
            {
                products = products.OrderBy(x => x.Price).ToList();
            }
            if (sort == "price_desc")
            {
                products = products.OrderByDescending(x => x.Price).ToList();
            }

            var result = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image) ? x.Image : "https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg",       
                Name = ShowProductName(x.Name),
                Brand= x.Brand,
                Price = x.Price,
                Discount = x.Discount,
                Status = x.Status,
            }).ToList();
            return View(result);
        }

        public string ShowProductName(string productName)
        {
            if(productName == "Iphone 14 Pro Max") {
                return productName + " [HOT]";
            }
            return productName;
        }

        // Create
        public IActionResult Create()
        {
            #region
            //var listBrands = new List<ComboboxDto> { };
            //var brands = _context.Brands.ToList();
            //foreach (var item in brands)
            //{
            //    var obj = new ComboboxDto { Id = item.Id, Name = item.Name };
            //    listBrands.Add(obj);
            //}
            #endregion

            var listBrands = _context.Brands.Select(x => new ComboboxDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            ViewBag.Brands = listBrands;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductModel model)
        {
            var productAdd  = new Product { 
                Name = model.Name,
                Price = model.Price,
                Brand = model.Brand,
                Discount = model.Discount,
                Status = model.Status
            };
            _context.Products.Add(productAdd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //Update
        [HttpGet]
        public ActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            //if (product == null)
            //{
            //    return RedirectToAction("NotFound", "Common");
            //}
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(int id, string name, double price, double discount,
            bool status)
        {
            //string CompleteUrl = this.Request.Url.AbsoluteUri;
            var product = _context.Products.Find(id);
            if (product == null)
            {
                ViewBag.Message = "San pham khong ton tai";
                return View();
            }
            product.Update(name, price, discount, status);
            _context.SaveChanges();
            return RedirectToAction("Update", new {id});
        }

        //// Up ảnh
        //[HttpPost]
        //public IActionResult Upload(HttpPostedFileBase file)
        //{
        //    return View();
        //}
    }

}
