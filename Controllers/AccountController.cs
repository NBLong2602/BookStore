using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
		public IActionResult CheckLogin(string username, string password)
		{
			return RedirectToAction("Index","Home");
		}
		//public IActionResult Logout
		//public IActionResult Register
	}
}
