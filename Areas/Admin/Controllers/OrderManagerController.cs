using BookStore.Areas.Admin.ViewModel;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Order")]
    public class OrderManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult OrderList()
        {
            var lstOrder = _context.OrderInfos.ToList();
            return View(lstOrder);
        }
    }
}
