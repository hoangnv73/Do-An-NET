using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
                BrandId = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                Status = x.Status,
                BrandName = ShowBrandName(x.BrandId),
            }).ToList();
            return View(result);
        }
        public string ShowBrandName(int? brandId)
        {
            if(brandId == null)
            {
                return "-";
            }
            else
            {
                //đã có brandId
                //Brand
                //-> Name
                var brand = _context.Brands.Find(brandId);
                string brandName = brand.Name;
                return brandName;
            }          
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
            #region Cach 1
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
            var product = new Product {
                Name = request.Name,
                Price = request.Price,
                BrandId = request.BrandId,
                //Biến = (điều kiện )? (Lệnh1 thực thi nếu đk đúng) : (lệnh 2 thực thi nếu đk sai);
                // nếu như request.Discount not null thì giá trị = chính nó --> nếu như null thì gắn = 0
                //Discount = request.Discount ?? 0,
                Discount = request.Discount == null ? 0 : request.Discount,
                Status = request.Status,
                Description = request.Description
            };
            _context.Products.Add(product);
            _context.SaveChanges();

            //-- nếu như ảnh not null thì cập nhật ảnh trong db & lưu ảnh trong folder 
            if (request.File != null)
            {
                string image = GetImage(product.Id, request.File);
                product.Image = image;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Delete Sweetarlet
        [HttpPost]
        public async Task<bool> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                var rootFolder = Directory.GetCurrentDirectory();

                string pathproduct = @$"{rootFolder}\wwwroot\data\{id}";
                //--exists kiểm tra thư mục tồn tại có tồn tại hay không
                if (Directory.Exists(pathproduct))
                {
                    //-- thêm true để xoá những folder có file
                    //-- nếu không có true thì chỉ xoá dược những folder rỗng 
                    Directory.Delete(pathproduct, true);
                }
               
               
                _context.SaveChanges();
            }
            else
            {
                //todo
            }
            return RedirectToAction("Index");
        }
        //Update
        [HttpGet]
        public ActionResult Update(int id)
        {
            var listBrands = _context.Brands.Select(x => new ComboboxDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            ViewBag.Brands = listBrands;

            var product = _context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(product);
        }
       
        [HttpPost]
        public ActionResult Update(int id, string name, float price, float discount, int brandId,
           bool status, IFormFile? file)
        {
            //string CompleteUrl = this.Request.Url.AbsoluteUri;
            var product = _context.Products.Find(id);
            if (product == null)
            {
                ViewBag.Message = "San pham khong ton tai";
                return View();
            }

            var image = product.Image;
            if (file != null)
            {
               image = GetImage(product.Id, file);
            }

            //Dòng 171: đây là function Update, a cố tình copy qua cho dễ nhìn
            //Update(string name, float price, float? discount, bool status, string? image, int brandid)
            // Dòng product.Update ... dưới là đoạn code gọi tới funtion Update ở trong class Product
            //thì mình phải truyền vào theo đúng thứ tự
            //nó mapping theo param truyền vào chứ ko phải theo cái name như kiểu ở input html
            // anh cố tình tạo 1 biến nameTest cho khác tên truyền vào vẫn được đó thấy không
            
            product.Update(name, price, discount, status, image, brandId,"abc");
           
            _context.SaveChanges();
            return RedirectToAction("Update", new { id });
        }

        public string GetImage(int productId, IFormFile file)
        {
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
