using BookStore.Infrastructure;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly BookStoreContext _context;

        public CartController(BookStoreContext ctx)
        {
            _context = ctx;
        }
        public IActionResult Index()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            HttpContext.Session.SetJson("cart", Cart);
            return View("Index", Cart);
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
                if(type == "ajax")
                {
                    return Json(new
                    {
                        quantity = Cart.lineCollection.Sum(c => c.Quantity)
                    });
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult ReduceToCart(int productId)
        {

            Book? product = _context.Books
            .FirstOrDefault(p => p.Isbn == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.ReduceItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int productId)
        {
            Book? product = _context.Books
            .FirstOrDefault(p => p.Isbn == productId);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            HttpContext.Session.SetJson("cart", Cart);
            return View(Cart);
        }
        //public IActionResult Payment


    }
}
