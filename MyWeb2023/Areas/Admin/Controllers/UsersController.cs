  using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using MyWeb2023.Areas.Admin.Models;
using MyWeb2023.Areas.Admin.Models.Dto;
using MyWeb2023.Models;
using System.Reflection;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
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
                Image = !string.IsNullOrEmpty(x.Image)
                        ? $"/data/users/{x.Id}/{x.Image}"
                        : "/data/default.png",
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                GenderName = x.Gender == null ? "-" : x.Gender == true ? "Nam" : "Nữ", //ifelseif
                RoleId = x.RoleId,
                RoleName = x.RoleId == null ? "-" : ShowRoleName(x.RoleId.Value)
            }).ToList();
            return View(result);
        }

        public string ShowRoleName(int roleId)
        {
            var role = _context.Roles.Find(roleId);
            return role == null ? "-" : role.Name;
        }

        [HttpPost]
        public async Task<string> ResetPassword(int id)
        {
            var user = _context.Users.Find(id);
            var newPassword = RandomText();
            user.UpdatePassword(CommonFunction.HashPassword(newPassword));
            _context.SaveChanges();
            return newPassword;
        }

        [HttpPost]
        public async Task<bool> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        // Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public string RandomText()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new string(stringChars);
            return finalString;
        }

        // Create
        public IActionResult Create()
        {
            var listRoles = _context.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            ViewBag.Roles = listRoles;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            var UserAdd = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = CommonFunction.HashPassword(model.Password),
                Email = model.Email,
                Gender = model.Gender,
                RoleId = model.RoleId,
                LastLogin = DateTime.Now,
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


        //Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            var listRoles = _context.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            ViewBag.Roles = listRoles;

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("NotFound", "Common");
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(int id, string firstname, string lastname, string password,
            bool gender, int statusid, IFormFile? file, int? roleId)
        {
            var user = _context.Users.Find(id);
            if (user == null) return RedirectToAction("NotFound", "Common");
            if (file != null)
            {
                //  lấy ra đường dẫn thư mục hiện tại
                var rootFolder = Directory.GetCurrentDirectory();
                var photoName = user.Image;
                string pathproduct = @$"{rootFolder}\wwwroot\data\users\{id}\" + photoName;
                System.IO.File.Delete(pathproduct);
                user.Image = file.FileName;
                CommonFunction.UploadImage(file, $"users/{user.Id}");
            }
            user.Update(firstname, lastname, password, gender, statusid, user.Image, roleId);
            _context.SaveChanges();

            return RedirectToAction("Update", new { id });
        }


        public string GetImage(int UserId, IFormFile file)
        {
            //// Get the current directory.
            var rootFolder = Directory.GetCurrentDirectory();
            //-- khai báo đường dẫn
            string pathUser = @$"{rootFolder}\wwwroot\data\users\{UserId}";

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