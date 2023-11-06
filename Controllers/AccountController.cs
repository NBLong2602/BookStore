using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public class AccountController : Controller
	{
		private readonly BookStoreContext _context;

		public AccountController(BookStoreContext ctx)
		{
			_context = ctx;
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult Login(Account account)
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				var u = _context.Accounts.Where(x => x.Username.Equals(account.Username) && x.Password.Equals(account.Password))
					.FirstOrDefault();
				if (u != null)
				{
					HttpContext.Session.SetString("UserName", u.Username.ToString());
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}
		public IActionResult Logout(){
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
		//public IActionResult Register
	}
}
