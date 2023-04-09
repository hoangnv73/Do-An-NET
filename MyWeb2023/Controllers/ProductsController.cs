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

            //var result = new List<ProductDto> { };
            //foreach (var item in products)
            //{

            //    var add = new ProductDto
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Brand = item.Brand,
            //        Price = item.Price,
            //        Discount = item.Discount,
            //        Quantity = item.Quantity,
            //    };
            //    result.Add(add);
            //}
            var result = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image) ? x.Image : "https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg",       
                Name = ShowProductName(x.Name),
                Brand= x.Brand,
                Price = x.Price,
                Discount = x.Discount,
                Quantity = x.Quantity,
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
                Quantity = model.Quantity
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
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(int Id, string name, double price, double discount, int quantity)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                ViewBag.Messager = "San pham khong ton tai";
                return View();
            }
            product.Update(name, price, discount, quantity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
