using BookStore.Infrastructure;
using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authentication]
    public class PaymentController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly BookStoreContext _context;
        public PaymentController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        //public IActionResult Index()
        //{
        //    string notification = TempData["ResutlPayment"] as string;
        //    ViewBag.ResutlPayment = notification;
        //    TempData.Remove("ResutlPayment");
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection form, float totalPrice)
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart");
            string? Note = form["note"];
            // Kiểm tra xem cart có tồn tại và có ít nhất một CartLine không
            if (Cart == null || !Cart.lineCollection.Any())
            {
                return RedirectToAction("PaymentFalse");
            }
            else
            {
                DateTime currentTime = DateTime.Now;
                //string formattedTime = currentTime.ToString("dd/MM/yyyy");
                //Add new OrderInfor
                if (int.TryParse(HttpContext.Session.GetString("UserId"), out int customerId))
                {
                    var orderInfo = new OrderInfo
                    {
                        CustomerId = customerId,
                        //EmployeeId = 1,
                        OrderDate = currentTime,
                        TotalPrice = totalPrice
                    };
                    _context.OrderInfos.Add(orderInfo);
                    await _context.SaveChangesAsync();
                    await AddOrderDetailAsync(orderInfo, Cart, currentTime, Note);
                    return RedirectToAction("PaymentSuccess");
                }
                else
                {
                    return RedirectToAction("PaymentFalse");
                }
            }
        }
        private async Task AddOrderDetailAsync(OrderInfo orderInfo, Cart cart, DateTime currentTime, string note)
        {
            var orderDetails = cart.lineCollection.Select(cartLine => new OrderDetail
            {
                BookIsbn = cartLine.Book.Isbn,
                OrderId = orderInfo.Id,
                TransactionDate = currentTime,
                Quantity = cartLine.Quantity,
                Note = note
            }).ToList();
            _context.OrderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();
        }
        public ActionResult PaymentSuccess()
        {
            HttpContext.Session.SetJson("cart", null);
            return View();
        }
        public ActionResult PaymentFalse()
        {
            return View();
        }
    }
}
