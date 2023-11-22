using BookStore.Models;
using BookStore.ViewModel;
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
            if (HttpContext.Session.GetString("UserId") == null)
            {
                var u = _context.Accounts.Where(x => x.Username.Equals(account.Username) && x.Password.Equals(account.Password))
                    .FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    HttpContext.Session.SetString("UserId", u.CustomerId.ToString());
                    string sessionId = HttpContext.Session.GetString("UserId").ToString();
                    int Adminrole = _context.Customers.Count(x => x.Id == int.Parse(sessionId) && x.CustomerTypeId == 1);
                    if (Adminrole > 0)
                    {
                        HttpContext.Session.SetString("AdRole", "Admin");
                        //return RedirectToAction("action", "controller", new { area = "area" });
                    }
                    else
                    {
                        HttpContext.Session.SetString("AdRole", "User");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CustomerAccountViewModel viewModel)
        {
            //Add new Customer
            var newCustomer = new Customer
            {
                CustomerTypeId = 2,
                Gender = true,
                Email = viewModel.Email
            };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            //Get Id newCustomer
            var newCustomerId = newCustomer.Id;
            //Add new Account with Id newCustomer
            var newAccount = new Account
            {
                Username = viewModel.Username,
                Password = viewModel.Password,
                CustomerId = newCustomerId
            };
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("AdRole");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
