using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
