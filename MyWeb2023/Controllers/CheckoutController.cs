using Microsoft.AspNetCore.Mvc;
using MyWeb.Infrastructure.Repositories;

namespace MyWeb2023.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IUserRepository _userRepository;

		public CheckoutController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<IActionResult> Index()
        {
            var a = await _userRepository.AddAsync("phong");
            return View();
        }
    }
}
