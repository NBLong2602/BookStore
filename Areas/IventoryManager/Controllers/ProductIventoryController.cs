using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.IventoryManager.Controllers
{
    [Area("IventoryManager")]
    [Route("Iventory/Product")]
    public class ProductIventoryController : Controller
    { 
        private readonly BookStoreContext _context;

        public ProductIventoryController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
