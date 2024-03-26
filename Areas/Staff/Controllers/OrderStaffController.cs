using BookStore.Areas.Models.Authentication;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Route("Staff/Manager/Order")]
    [Authentication]
    [StaffAuthorize]
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

        [Route("Filter")]
        public IActionResult FilterOrder(int daysAgo)
        {
            if (daysAgo == 999)
            {
               return RedirectToAction("OrderList");
            }
            else
            {
                DateTime currentTime = DateTime.Now.AddDays(-daysAgo).Date;
                var lstFilterOrder = _context.OrderInfos
                                   .Where(x => x.OrderDate >= currentTime)
                                   .ToList();
                return View("OrderList", lstFilterOrder);
            }


        }

        [Route("Accept")]
        public IActionResult AcceptOrder(int orderId)
        {
            var order = _context.OrderInfos
                        .Include(o => o.OrderDetails) // Nạp danh sách các chi tiết đơn hàng
                            .ThenInclude(od => od.BookIsbnNavigation)
                        .Where(x => x.Id.Equals(orderId)).FirstOrDefault();
            if (order.Id != orderId)
            {
                return RedirectToAction("OrderList");
            }
            else
            {
                var EmployeeId = HttpContext.Session.GetString("AccountId");
                order.EmployeeId = int.Parse(EmployeeId);
                var lstBookIsbn = order.OrderDetails.Select(item => item.BookIsbn).ToList();
                var booksToUpdate = _context.Books.Where(book => lstBookIsbn.Contains(book.Isbn)).ToList();

                foreach (var item in order.OrderDetails)
                {
                    var book = booksToUpdate.FirstOrDefault(x => x.Isbn == item.BookIsbn);
                    if (book != null)
                    {
                        book.Stock -= (int)item.Quantity;
                    }
                }
                _context.UpdateRange(booksToUpdate);
                _context.Update(order);
                _context.SaveChanges();
                return RedirectToAction("OrderList");

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
