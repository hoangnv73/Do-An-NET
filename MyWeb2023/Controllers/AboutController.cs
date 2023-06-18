using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
