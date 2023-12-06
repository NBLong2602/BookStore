using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Register(CustomerAccountViewModel viewModel)
        {
            //Check trùng Mail & Username
            bool checkEmail = await _context.Customers.AnyAsync(x => x.Email == viewModel.Email);
            bool checkUsername = await _context.Accounts.AnyAsync(x => x.Username == viewModel.Username);

            if (checkEmail == false && checkUsername == false)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Add new Customer
                        var newCustomer = new Customer
                        {
                            CustomerTypeId = 2,
                            Gender = true,
                            Email = viewModel.Email
                        };
                        _context.Customers.Add(newCustomer);
                        await _context.SaveChangesAsync();

                        // Add new Account with Id newCustomer
                        var newAccount = new Account
                        {
                            Username = viewModel.Username,
                            Password = viewModel.Password,
                            CustomerId = newCustomer.Id
                        };
                        _context.Accounts.Add(newAccount);
                        await _context.SaveChangesAsync();

                        // Commit transaction if all operations succeed
                        transaction.Commit();
                        return RedirectToAction("Login");
                    }
                    catch (Exception)
                    {
                        // Rollback transaction if there is an exception
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            else
            {
                ViewBag.Message = "Đăng ký trùng email hoặc username";
                return View();
            }
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
