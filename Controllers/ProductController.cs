using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly BookStoreContext _context;

        public ProductController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        
        public IActionResult AllProduct(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstProduct = _context.Books.AsNoTracking().OrderBy(x => x.BookName);
			PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult ProductByCategory(int categoryId, int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstProduct = _context.Books.AsNoTracking().Where(x => x.BookCategoryId == categoryId).OrderBy(x => x.PublishYear);
			PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
            ViewBag.categoryId = categoryId;
            return View("AllProduct", lst);
        }
        public IActionResult ProductDetail(int productId)
        {
            var product = _context.Books.Where(x => x.Isbn == productId).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string keywords, int? page)
        {
            if (keywords == null)
            {
                return RedirectToAction("AllProduct");
            }
            else
            {
                int pageSize = 12;
                int pageNumber = page == null || page < 0 ? 1 : page.Value;
                var lstProduct = _context.Books.Where(x => x.BookName.Contains(keywords));
				PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
                return View("AllProduct", lst);
            }

        }
    }
}
