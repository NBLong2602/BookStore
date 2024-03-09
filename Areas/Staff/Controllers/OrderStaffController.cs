using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            var order = _context.OrderInfos.Where(x => x.Id.Equals(orderId)).FirstOrDefault();
            if (order.Id != orderId)
            {
                return RedirectToAction("OrderList");
            }
            else
            {
                var EmployeeId = HttpContext.Session.GetString("EmployeeId");
                order.EmployeeId = int.Parse(EmployeeId);
                _context.OrderInfos.Update(order);
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

    }
}
