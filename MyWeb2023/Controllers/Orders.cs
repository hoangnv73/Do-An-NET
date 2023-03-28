using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class Orders : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
