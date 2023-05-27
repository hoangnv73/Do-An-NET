using Microsoft.AspNetCore.Mvc;
using Myweb.Domain.Models.Entities;
using System;
using System.IO;
namespace MyWeb2023.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            int productId = 1;
            string currentPath = @"D:\LAPTRINH\Project\MyWeb2023\MyWeb2023\data";
            string path = currentPath + @"wwwroot/uploads";
            string? imgUrl = null;
            if (file != null) imgUrl = UploadImage(productId, file);
            var pr = new Product
            {
                Name = "sds",
                Image = imgUrl
            };


            return View();
        }

        public string UploadImage(int productId, IFormFile file)
        {
            string pathProduct = @$"c:\MyDir\{productId}";
            if (!Directory.Exists(pathProduct))
            {
                Directory.CreateDirectory(pathProduct);
            }
            string fileName = file.FileName;
            var filePath = Path.Combine(pathProduct, fileName);

            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}
