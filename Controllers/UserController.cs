using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BookStore.Controllers
{
    [Authentication]
    public class UserController : Controller
    {
        private readonly BookStoreContext _context;

        public UserController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        public IActionResult Profile()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var ProfileUser = _context.Customers.Where(c => c.Id.Equals(sessionUserId)).FirstOrDefault();
            return View(ProfileUser);
        }
        public IActionResult Purchase()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var OrderInfos = _context.OrderInfos.Where(o => o.CustomerId.Equals(sessionUserId)).ToList();
            return View(OrderInfos);
        }
        //public IActionResult Address()
        //{
        //    return View();
        //}
        //public IActionResult Payment()
        //{
        //    return View();
        //}
        //public IActionResult EditProfile()
        //{
        //    return View();
        //}
        //public IActionResult Purchase() - Order History
        //[Route("/account")]
        //[Route("/Profile")]

        //
        //[Route("/Address")]
        //public IActionResult Address()
        //[Route("/Payment")]
        //public IActionResult Payment()

    }
}
