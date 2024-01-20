using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Globalization;

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

        [Route("Profile/Edit")]
        [HttpGet]
        public IActionResult ProfileEdit()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var ProfileUser = _context.Customers.Where(c => c.Id.Equals(sessionUserId)).FirstOrDefault();
            return View(ProfileUser);
        }
        [Route("Profile/Edit")]
        [HttpPost]
        public IActionResult ProfileEdit(IFormCollection form)
        {

            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var customerToUpdate = _context.Customers.Find(sessionUserId);
            if (customerToUpdate != null)
            {
                string fullname = form["fullName"];
                if (!string.IsNullOrEmpty(fullname))
                {
                    customerToUpdate.FullName = fullname;
                }

                string checkgender = form["gender"];
                if (!string.IsNullOrEmpty(form["gender"]))
                {
                    if (checkgender == "Nu")
                        customerToUpdate.Gender = false;
                    else customerToUpdate.Gender = true;
                }

                string birthdayString = form["birthday"];
                DateTime birthDay;
                if (!string.IsNullOrEmpty(birthdayString) && DateTime.TryParse(birthdayString, out birthDay))
                {
                    customerToUpdate.Birthday = birthDay;
                }

                string email = form["email"];
                if (!string.IsNullOrEmpty(email))
                {
                    customerToUpdate.Email = email;
                }

                string phone = form["phone"];
                if (!string.IsNullOrEmpty(phone) && phone.Length == 10)
                {
                    customerToUpdate.Phone = phone;
                }

                _context.Customers.Update(customerToUpdate);
                _context.SaveChanges();
            }

            return RedirectToAction("Profile");
        }

        [Route("Purchase")]
        public IActionResult Purchase()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var OrderInfos = _context.OrderInfos.Where(o => o.CustomerId.Equals(sessionUserId)).OrderByDescending(x => x.Id).ToList();
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

            return RedirectToAction("Address");
        }




    }
}
