using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
