using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            //return Content("Đăng Nhập Thành Công");
            return Redirect("Home");
        }
    }
}
