using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BookStore.Controllers
{
    [Authentication]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly BookStoreContext _context;

        public UserController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        [Route("Profile")]
        public IActionResult Profile()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var ProfileUser = _context.Customers.Where(c => c.Id.Equals(sessionUserId)).FirstOrDefault();
            return View(ProfileUser);
        }
        [Route("Purchase")]
        public IActionResult Purchase()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var OrderInfos = _context.OrderInfos.Where(o => o.CustomerId.Equals(sessionUserId)).OrderByDescending(x=>x.Id).ToList();
            return View(OrderInfos);
        }
        [Route("Address")]
        public IActionResult Address()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var AddressUser = _context.Customers.Where(c => c.Id.Equals(sessionUserId)).FirstOrDefault();
            return View(AddressUser);
        }

        [Route("Address/Edit")]
        public IActionResult AddressEdit()
        {
            return View();
        }



    }
}
