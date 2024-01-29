using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Customer")]
    public class CustomerManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public CustomerManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult CustomerList()
        {
            var lstCustomer = _context.Customers
                               .AsNoTracking()
                               .Where(x => x.CustomerTypeId == 2)
                               .OrderBy(x => x.Id)
                               .ToList();
            return View(lstCustomer);
        }

        [Route("Detail")]
        [HttpGet]
        public IActionResult CustomerDetail(int customerId)
        {

            var Customer = _context.Customers
                               .AsNoTracking()
                               .Where(x => x.Id == customerId)
                               .FirstOrDefault();

            return View(Customer);
        }
    }
}
