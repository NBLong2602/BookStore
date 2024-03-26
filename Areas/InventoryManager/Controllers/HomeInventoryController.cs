using BookStore.Areas.Models.Authentication;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.IventoryManager.Controllers
{
    [Area("InventoryManager")]
    [Route("Inventory")]
    [Route("Inventory/Home")]
    [Authentication]
    [InventoryAuthorize]
    public class HomeInventoryController : Controller
    {
        private readonly BookStoreContext _context;

        public HomeInventoryController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var lstNhapHang = _context.IventoryProducts
                              .AsNoTracking()
                              .Include(x => x.MaSachNavigation)
                              .Include(x => x.User)
                              .OrderBy(x => x.MaSach)
                              .ToList();
            return View(lstNhapHang);
        }

        [Route("")]
        [Route("ListProduct")]
        public IActionResult ListProduct()
        {
            var lstProduct = _context.Books
                               .AsNoTracking()
                               .Include(categories => categories.BookCategory)
                               .OrderBy(x => x.Isbn)
                               .ToList();
            return View(lstProduct);
        }

    }
}
