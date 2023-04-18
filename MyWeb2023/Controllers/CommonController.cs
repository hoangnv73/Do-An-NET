using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
