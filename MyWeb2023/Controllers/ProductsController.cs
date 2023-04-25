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

            var result = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/{x.Id}/{x.Image}" 
                        : "/www/images/default-thumbnail.jpg",       
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
        public IActionResult Create(CreateProductModel request)
        {
            var product  = new Product { 
                Name = request.Name,
                Price = request.Price,
                Brand = request.Brand,
                //Biến = (điều kiện )? (Lệnh1 thực thi nếu đk đúng) : (lệnh 2 thực thi nếu đk sai);
                Discount = request.Discount == null ? 0 : request.Discount,
                // nếu như request.Discount not null thì giá trị = chính nó --> nếu như null thì gắn = 0
                //Discount = request.Discount ?? 0,
                Status = request.Status
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            //-- nếu như ảnh not null thì cập nhật ảnh trong db & lưu ảnh trong folder 
            if (request.File != null)
            {
                string image = GetImage(product.Id, request.File);
                // update
                product.Image = image;
                _context.SaveChanges();
            }
            
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

            if (product == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Update(int id, string name, double price, double discount,
            bool status, IFormFile file)
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

        public string GetImage(int productId, IFormFile file)
        {

            //if (file == null) return null;

            //// Get the current directory.
            var rootFolder = Directory.GetCurrentDirectory();
            //-- khai báo đường dẫn
            string pathproduct = @$"{rootFolder}\wwwroot\data\{productId}";

            //-- Kiểm tra folder đã tồn tại hay chưa
            if (!Directory.Exists(pathproduct))
            {
                //-- Nếu chưa có thì tạo mới
                Directory.CreateDirectory(pathproduct);
            }
            string filename = file.FileName;
            var filepath = Path.Combine(pathproduct, filename);

            using (FileStream filestream = System.IO.File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
            return filename;
        }
    }

}
