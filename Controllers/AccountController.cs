using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("Identity/Account")]
    public class AccountController : Controller
    {
        [Route("Login")]
        public IActionResult Login(string username, string password)
        {
            return View();
        }
        bool CheckLogin(string username, string password)
        {
            return true;
        }
        //public IActionResult Logout
        //public IActionResult Register
    }
}
