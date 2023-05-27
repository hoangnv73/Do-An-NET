using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
