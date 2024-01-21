using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly BookStoreContext _context;

        public ProductController(BookStoreContext ctx)
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

        // Add more actions as needed...
    }
}
