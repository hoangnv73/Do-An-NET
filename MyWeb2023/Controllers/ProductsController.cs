using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
