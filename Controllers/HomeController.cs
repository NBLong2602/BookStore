using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookStoreContext _context;

        public HomeController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber=page==null||page<0?1:page.Value;
            var lstProduct = _context.Books.AsNoTracking().OrderBy(x=>x.BookName);
            PagedList<Book> lst = new PagedList<Book>(lstProduct,pageNumber,pageSize);
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}