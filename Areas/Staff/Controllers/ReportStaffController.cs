using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("Staff/Report")]
    public class ReportStaffController : Controller
    {
        private readonly BookStoreContext _context;

        public ReportStaffController(BookStoreContext ctx)
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
