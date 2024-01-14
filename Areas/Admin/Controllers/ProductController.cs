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
                               .Include(categories => categories.BookCategory) // Bổ sung Include để tải thông tin tác giả
                               .OrderBy(x => x.BookName)
                               .ToList();
            return View(lstProduct);
        }
    }
}
