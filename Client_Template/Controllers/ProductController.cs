using Microsoft.AspNetCore.Mvc;

namespace Client_Template.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
