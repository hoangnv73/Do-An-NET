using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Models;
using MyWeb2023.Models.Dto;

namespace MyWeb2023.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            var result = users.Select(x => new UserDto
            {
                Id = x.Id,
                Image = x.Image,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                GenderName = x.Gender == null ? "-" : x.Gender == true ? "Nam" : "Nữ" //ifelseif
            }).ToList();
            return View(result);
        }
        // Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            var UserAdd = new User {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email,
                Gender = model.Gender,
            };
            _context.Users.Add(UserAdd);
            _context.SaveChanges();

            //-- nếu như ảnh not null thì cập nhật ảnh trong db & lưu ảnh trong folder 
            if (model.File != null)
            {
                string image = GetImage(UserAdd.Id, model.File);
                UserAdd.Image = image;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges(true);
            return RedirectToAction("Index");
        }

        public string GetImage(int UserId, IFormFile file)
        {
            //// Get the current directory.
            var rootFolder = Directory.GetCurrentDirectory();
            //-- khai báo đường dẫn
            string pathUser = @$"{rootFolder}\wwwroot\data\users{UserId}";

            //-- Kiểm tra folder đã tồn tại hay chưa
            if (!Directory.Exists(pathUser))
            {
                //-- Nếu chưa có thì tạo mới
                Directory.CreateDirectory(pathUser);
            }
            string filename = file.FileName;
            var filepath = Path.Combine(pathUser, filename);

            using (FileStream filestream = System.IO.File.Create(filepath))
            {
                file.CopyTo(filestream);
            }
            return filename;
        }
    }
}