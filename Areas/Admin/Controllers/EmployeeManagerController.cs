using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Employee")]
    public class EmployeeManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public EmployeeManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult EmployeeList()
        {
            var lstEmployee = _context.Employees
                               .AsNoTracking()
                               .OrderBy(x => x.Id)
                               .ToList();
            return View(lstEmployee);
        }

        [Route("Add")]
        [HttpGet]
        public IActionResult AddEmployee()
        {
            ViewBag.CustomTypeId = new SelectList(_context.CustomerTypes.ToList(), "Id", "Type");
            return View();
        }

        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                ViewBag.SuccessMessage = "Thêm nhân viên thành công!";
                return RedirectToAction("EmployeeList", "EmployeeManager");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // In ra console hoặc log để kiểm tra lỗi chi tiết
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult EditEmployee(int employeeId)
        {
            ViewBag.CustomTypeId = new SelectList(_context.CustomerTypes.ToList(), "Id", "Type");
            var employee = _context.Employees.Find(employeeId);
            return View(employee);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(Employee model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("EmployeeList", "EmployeeManager");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // In ra console hoặc log để kiểm tra lỗi chi tiết
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

        }

        [Route("Detail")]
        [HttpGet]
        public IActionResult EmployeeDetail(int employeeId)
        {

            var employee = _context.Employees
                               .AsNoTracking()
                               .Where(x => x.Id == employeeId)
                               .FirstOrDefault();

            return View(employee);
        }

        [Route("Remove")]
        [HttpGet]
        public IActionResult RemoveEmployee(int employeeId)
        {
            TempData["Message"] = "";
            var checkPurchase = _context.OrderInfos.Where(x => x.EmployeeId == employeeId).ToList();
            if (checkPurchase.Count > 0)
            {
                TempData["Message"] = "Khong The Xoa Do Anh Huong Du Lieu He Thong";
                return RedirectToAction("index", "HomeAdmin");

            }
            else
            {
                _context.Remove(_context.Employees.Find(employeeId));
                _context.SaveChanges();
                TempData["Message"] = "Xoa Thanh Cong";
                return RedirectToAction("EmployeeList", "EmployeeManager");
            }
        }
    }
}
