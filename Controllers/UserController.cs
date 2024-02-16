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

        #region Profile

        [Route("Profile")]
        public IActionResult Profile()
        {
            if (int.TryParse(HttpContext.Session.GetString("UserId"), out int sessionUserId))
            {
                var profileUser = _context.Customers
                                           .FirstOrDefault(c => c.Id == sessionUserId);

                if (profileUser != null)
                {
                    return View(profileUser);
                }
            }

            // Xử lý trường hợp không thể chuyển đổi "UserId" thành số nguyên
            // hoặc không tìm thấy người dùng trong database.
            // Có thể thêm thông báo lỗi hoặc chuyển hướng đến trang lỗi tùy thuộc vào yêu cầu của bạn.
            return RedirectToAction("Error");

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
        #endregion

        #region Address

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
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var customerToUpdate = _context.Customers.Find(sessionUserId);
            if (customerToUpdate != null)
            {
                customerToUpdate.Address = location;
                _context.Customers.Update(customerToUpdate);
                _context.SaveChanges();
                return RedirectToAction("Address", "User");
            }
            else
            {
                return View();
            }    
            

        }

        #endregion

        [Route("Purchase")]
        public IActionResult Purchase()
        {
            int sessionUserId = int.Parse(HttpContext.Session.GetString("UserId").ToString());
            var OrderInfos = _context.OrderInfos.Where(o => o.CustomerId.Equals(sessionUserId)).OrderByDescending(x => x.Id).ToList();
            return View(OrderInfos);
        }


    }
}
