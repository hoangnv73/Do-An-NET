﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models.Dto;
using MyWeb2023.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public async Task<IActionResult> Index(string sort, int? page, int? brandId)
        {
            var products = await _context.Products.ToListAsync();
            if (brandId != null)
            {
                products = products.Where(x => x.BrandId == brandId).ToList();
            }

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
                        ? $"/data/products/{x.Id}/{x.Image}"
                        : "/data/default.png",
                Name = x.Name,
                BrandId = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                Status = x.Status,
                Description = x.Description,
                BrandName = ShowBrandName(x.BrandId),
            }).ToList();
            return View(result);
        }
        public string ShowBrandName(int? brandId)
        {
            if (brandId == null)
            {
                return "-";
            }
            else
            {
                var brand = _context.Brands.Find(brandId);
                string brandName = brand.Name;
                return brandName;
            }
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
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                BrandId = request.BrandId,
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

        // Delete
        [HttpPost]
        public async Task<bool> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                var rootFolder = Directory.GetCurrentDirectory();

                string pathproduct = @$"{rootFolder}\wwwroot\data\products\{id}";
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
            return true;
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

            var categories = _context.Categories.Select(x => new ComboboxDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            ViewBag.Brands = listBrands;
            ViewBag.Categories = categories;

            var product = _context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Update(int id, string name, float price, float discount, int brandId,
           bool status, IFormFile? file, string description, int? categoryId)
        {
            var product = _context.Products.Find(id);
            // delete ảnh cũ trong folder
            var rootFolder = Directory.GetCurrentDirectory();
            var photoName = product.Image;
            
            if (product == null)
            {
                ViewBag.Message = "San pham khong ton tai";
                return View();
            }

            var image = product.Image;

            //nếu file = null thì không cho cập nhật image
            if (file != null)
            {
                string pathproduct = @$"{rootFolder}\wwwroot\data\products\{id}\" + photoName;
                try
                {
                    System.IO.File.Delete(pathproduct);
                }
                catch
                {

                }
                image = GetImage(product.Id, file);
            }
            
            product.Update(name, price, discount, status, image, brandId, description, categoryId);
            _context.SaveChanges();
            return RedirectToAction("Update", new { id });
        }

        public string GetImage(int productId, IFormFile file)
        {
            //// Get the current directory.
            var rootFolder = Directory.GetCurrentDirectory();
            //-- khai báo đường dẫn
            string pathproduct = @$"{rootFolder}\wwwroot\data\products\{productId}";

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
