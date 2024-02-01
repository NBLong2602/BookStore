using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Category")]
    public class CategoryManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public CategoryManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult CategoryList()
        {
            var lstCate = _context.BookCategories
                               .AsNoTracking()
                               .OrderBy(x => x.Name)
                               .ToList();
            return View(lstCate);
        }

        [Route("Add")]
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(BookCategory model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        {
            if (ModelState.IsValid)
            {
                _context.BookCategories.Add(model);
                _context.SaveChanges();
                ViewBag.SuccessMessage = "Thêm danh mục thành công!";
                return RedirectToAction("CategoryList", "CategoryManager");
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

        [Route("Remove")]
        [HttpGet]
        public IActionResult RemoveCategory(int categoryId)
        {
            TempData["Message"] = "";
            var checkPurchase = _context.OrderDetails.Where(x => x.BookIsbnNavigation.BookCategoryId == categoryId).ToList();
            if (checkPurchase.Count > 0)
            {
                TempData["Message"] = "Khong The Xoa San Pham Nay Do Anh Huong Du Lieu He Thong";
                return RedirectToAction("index", "HomeAdmin");
            }
            else
            {
                _context.Remove(_context.BookCategories.Find(categoryId));
                _context.SaveChanges();
                TempData["Message"] = "Xoa Thanh Cong";
                return RedirectToAction("CategoryList", "CategoryManager");
            }
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult CategoryEdit(int categoryId)
        {
            var cate = _context.BookCategories.Find(categoryId);

            if (cate == null)
            {
                // Handle when the product is not found
                return NotFound();
            }

            return View(cate);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CategoryEdit(BookCategory model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("CategoryList", "CategoryManager");
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


    }
}
