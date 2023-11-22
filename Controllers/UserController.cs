using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Purchase()
        {
            return View();
        }

        //public IActionResult Purchase() - Order History
        //[Route("/account")]
        //[Route("/Profile")]
        public IActionResult Profile()
        {
            return View();
        }
        //public IActionResult EditProfile()
        //[Route("/Address")]
        //public IActionResult Address()
        //[Route("/Payment")]
        //public IActionResult Payment()

    }
}
