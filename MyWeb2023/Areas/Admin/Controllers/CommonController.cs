using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
