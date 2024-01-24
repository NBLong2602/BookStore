using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductAdminController : Controller
    {
        private readonly BookStoreContext _context;

        public ProductAdminController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult ProductList()
        {
            var lstProduct = _context.Books
                               .AsNoTracking()
                               .Include(categories => categories.BookCategory)
                               .OrderBy(x => x.Isbn)
                               .ToList();
            return View(lstProduct);
        }

        [Route("Filter/categoryId-{categoryId}")]
        [HttpGet]
        public IActionResult FilterProduct(int categoryId)
        {
            var lstProduct = _context.Books
                               .AsNoTracking()
                               .Where(x => x.BookCategoryId == categoryId)
                               .Include(categories => categories.BookCategory)
                               .OrderBy(x => x.Isbn)
                               .ToList();
            return View("ProductList", lstProduct);
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int productId)
        {
            ViewBag.BookCategoryId = new SelectList(_context.BookCategories.ToList(), "Id", "Name");
            ViewBag.PublisherId = new SelectList(_context.Publishers.ToList(), "Id", "Name");
            ViewBag.AuthorId = new SelectList(_context.Authors.ToList(), "Id", "Name");
            var product = _context.Books.Find(productId);

            if (product == null)
            {
                // Handle when the product is not found
                return NotFound();
            }

            return View(product);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                _context.SaveChanges();
                return RedirectToAction("ProductList", "ProductAdmin");
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
        public IActionResult ProductDetail(int productId)
        {

            var product = _context.Books
                               .AsNoTracking()
                               .Where(x => x.Isbn == productId)
                               .Include(categories => categories.BookCategory)
                               .Include(author => author.Author)
                               .Include(publisher => publisher.Publisher)
                               .OrderBy(x => x.Isbn)
                               .FirstOrDefault();


            return View(product);
        }
    }
}
