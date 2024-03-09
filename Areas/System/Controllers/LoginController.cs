using BookStore.Areas.System.ViewModel;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.System.Controllers
{
    [Area("System")]
    [Route("Login")]
    [Route("System/Login")]
    public class LoginController : Controller
    {
        private readonly BookStoreContext _context;

        public LoginController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CustomerType = new SelectList(_context.CustomerTypes.ToList(), "Id", "Type");
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var roleLogged = HttpContext.Session.GetString("Role");
            //Chưa đăng nhập
            if (string.IsNullOrEmpty(roleLogged))
            {
                //staff - db Employees
                if (model.role == 4)
                {
                    var staff = _context.Employees.FirstOrDefault(x => x.Username.Equals(model.userName) && x.Password.Equals(model.password));
                    if (staff != null)
                    {
                        HttpContext.Session.SetString("Role", "Employee");
                        HttpContext.Session.SetString("EmployeeId", staff.Id.ToString());
                        return RedirectToAction("OrderList", "OrderStaff", new { area = "Staff" });
                    }
                }
                // admin = CustomerTypeId == 1 or inventory = CustomerTypeId == 2 - db Accounts
                else if (model.role == 1 || model.role == 2) 
                {
                    var user = _context.Accounts.FirstOrDefault(x => x.Username.Equals(model.userName) && x.Password.Equals(model.password));
                    if (user != null)
                    {
                        var checkRole = _context.Customers.FirstOrDefault(x => x.Id == user.CustomerId && x.CustomerTypeId == model.role);
                        if (checkRole != null)
                        {
                            if (model.role == 1)
                            {
                                HttpContext.Session.SetString("Role", "Admin");
                                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                            }
                            else if (model.role == 2)
                            {
                                HttpContext.Session.SetString("Role", "Inventory");
                                HttpContext.Session.SetString("InventoryId", user.CustomerId.ToString());
                                return RedirectToAction("Index", "HomeInventory", new { area = "InventoryManager" });
                            }
                        }
                    }
                }
                return RedirectToAction("Index", "Login", new { area = "System" });
            }
            //đã đăng nhập
            else
            {
                switch (roleLogged)
                {
                    case "staff":
                        return RedirectToAction("OrderList", "OrderStaff", new { area = "Staff" });
                    case "inventory":
                        return RedirectToAction("Index", "HomeInventory", new { area = "InventoryManager" });
                    case "admin":
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    default:
                        return RedirectToAction("Index", "Login", new { area = "System" });
                }
            }
        }

    }
}
