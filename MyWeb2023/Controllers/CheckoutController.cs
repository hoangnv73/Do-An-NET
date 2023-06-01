using Microsoft.AspNetCore.Mvc;
using MyWeb.Infrastructure.Repositories;

namespace MyWeb2023.Controllers
{
    public class CheckoutController : Controller
    {


		public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
