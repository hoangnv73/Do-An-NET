using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWeb2023.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class CommonController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
