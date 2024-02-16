using BookStore.Infrastructure;
using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Authentication]
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly BookStoreContext _context;
        decimal _feeShip = 35000;
        decimal _itemPrice = 0;
        decimal _totalPrice = 0;
        public CartController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        public IActionResult Index()
        {
            ViewBag.feeShip = _feeShip;
            ViewBag.itemPrice = _itemPrice;
            ViewBag.totalPrice = _totalPrice;
            Cart = HttpContext.Session.GetJson<Cart>("cart");
            return View(Cart);
        }
        public IActionResult Checkout()
        {
            ViewBag.feeShip = _feeShip;
            ViewBag.itemPrice = _itemPrice;
            ViewBag.totalPrice = _totalPrice;
            Cart = HttpContext.Session.GetJson<Cart>("cart");
            if (Cart != null)
            {
                return View(Cart);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult AddToCart(int productId, int quantity, string type = "Normal")
        {
            Book? product = _context.Books
            .FirstOrDefault(p => p.Isbn == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, quantity);
                HttpContext.Session.SetJson("cart", Cart);
                if (type == "ajax")
                {
                    return Json(new
                    {
                        quantity = Cart.lineCollection.Sum(c => c.Quantity)
                    });
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult ReduceToCart(int productId, int quantity, string type = "Normal")
        {
            Book? product = _context.Books
            .FirstOrDefault(p => p.Isbn == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.ReduceItem(product, quantity);
                HttpContext.Session.SetJson("cart", Cart);
                if (type == "ajax")
                {
                    return Json(new
                    {
                        quantity = Cart.lineCollection.Sum(c => c.Quantity)
                    });
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int productId, string type = "Normal")
        {
            Book? product = _context.Books
            .FirstOrDefault(p => p.Isbn == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
                if (type == "ajax")
                {
                    return Json(new
                    {
                        quantity = Cart.lineCollection.Sum(c => c.Quantity)
                    });
                }
            }
            return RedirectToAction("Index");
        }

    }
}
