using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("Staff/Manager/Order")]
    public class OrderStaffController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderStaffController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult OrderList()
        {
            var lstOrder = _context.OrderInfos.OrderBy(x => x.EmployeeId).ToList();
            return View(lstOrder);
        }

        //[Route("Filter/{datetime}")]
        //[HttpGet]
        //public IActionResult FilterProduct(int datetime)
        //{
        //    var lstOrder = _context.Books
        //                       .AsNoTracking()
        //                       .Where(x => x.BookCategoryId == categoryId)
        //                       .Include(categories => categories.BookCategory)
        //                       .OrderBy(x => x.Isbn)
        //                       .ToList();
        //    return View("OrderList", lstOrder);
        //}

        [Route("")]
        [Route("Accept")]
        [HttpPost]
        public bool AcceptOrder(int orderId)
        {
            var order = _context.OrderInfos.Where(x => x.Id.Equals(orderId)).FirstOrDefault();
            if (order.Id != orderId)
            {
                return false;
            }
            else
            {
                order.EmployeeId = 1;
                _context.OrderInfos.Update(order);
                _context.SaveChanges();
                return true;
            }

        }

        [Route("{orderId}")]
        public IActionResult OrderDetail(int orderId)
        {
            var order = _context.OrderInfos
                    .Include(o => o.Customer) // Nạp thông tin khách hàng
                    .Include(o => o.OrderDetails) // Nạp danh sách các chi tiết đơn hàng
                        .ThenInclude(od => od.BookIsbnNavigation)
                    .FirstOrDefault(x => x.Id == orderId);
            return View(order);
        }

    }
}
