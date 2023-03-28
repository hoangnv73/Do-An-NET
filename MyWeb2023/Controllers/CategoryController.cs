using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
