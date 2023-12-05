using BookStore.Infrastructure;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PaymentController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly BookStoreContext _context;
        public PaymentController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        public async Task<IActionResult> Payment()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart");
            // Kiểm tra xem cart có tồn tại và có ít nhất một CartLine không
            if (Cart == null || !Cart.lineCollection.Any())
            {
                ViewBag.ResutlPayment = "Đặt Hàng Thất Bại";
            }
            else
            {
                DateTime currentTime = DateTime.Now;
                // Định dạng theo định dạng MM/dd/yyyy
                string formattedTime = currentTime.ToString("MM/dd/yyyy");
                //Add new OrderInfor
                if (int.TryParse(HttpContext.Session.GetString("UserId"), out int customerId))
                {
                    var orderInfo = new OrderInfo
                    {
                        CustomerId = customerId,
                        EmployeeId = 1,
                        OrderDate = formattedTime
                    };
                    _context.OrderInfos.Add(orderInfo);
                    await _context.SaveChangesAsync();
                    await AddOrderDetailAsync(orderInfo, Cart, formattedTime);
                    ViewBag.ResutlPayment = "Đặt Hàng Thành Công";
                }
                else
                {
                    ViewBag.ResutlPayment = "Đặt Hàng Thất Bại";
                }
            }
            return View();
        }
        private async Task AddOrderDetailAsync(OrderInfo orderInfo, Cart cart, string formattedTime)
        {
            foreach (var cartLine in cart.lineCollection)
            {
                int quantity = cartLine.Quantity;
                int Ibsn = cartLine.Book.Isbn;
                var orderDetail = new OrderDetail
                {
                    BookIsbn = Ibsn,
                    OrderId = orderInfo.Id,
                    TransactionDate = formattedTime,
                    Quantity = quantity
                };
                _context.OrderDetails.Add(orderDetail);
            }
            await _context.SaveChangesAsync();
        }
    }
}
