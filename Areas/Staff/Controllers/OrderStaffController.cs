using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

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

        [Route("")]
        [Route("Order")]
        [HttpPost]
        public IActionResult AcceptOrder(int orderId)
        {
            // FE: Switch On/Off Trạng thái đơn hàng ( CHƯA DUYỆT THÌ CÓ SWITCH)
            // BE: employeeId = session nhân viên bán hàng => duyệt
            return View();
        }


    }
}
