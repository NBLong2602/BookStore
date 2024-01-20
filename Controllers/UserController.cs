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
        [HttpGet]
        public IActionResult AddressEdit()
        {
            return View();
        }
        [Route("Address/Edit")]
        [HttpPost]
        public IActionResult AddressEdit(string location)
        {

            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var customerToUpdate = _context.Customers.Find(sessionUserId);
            if (customerToUpdate != null)
            {
                string addressState = location;
                customerToUpdate.Address = addressState;
                _context.Customers.Update(customerToUpdate);
                _context.SaveChanges();
            }

            return RedirectToAction("Address") ;
        }


    }
}
