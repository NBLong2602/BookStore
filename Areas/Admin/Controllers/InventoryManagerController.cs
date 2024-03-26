using BookStore.Areas.Models.Authentication;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Inventory")]
    [Authentication]
    [AdminAuthorize]
    public class InventoryManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public InventoryManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult Index()
        {
            var lstInventory = _context.IventoryProducts
                                                .Include(x => x.MaSachNavigation)
                                                .Include(x => x.User)
                                                .OrderBy(x => x.SoLuongNhap)
                                                .Where(x => x.TrangThaiDuyet != 2)
                                                .ToList();
            return View(lstInventory);
        }

        [Route("View")]
        public IActionResult View(int InvenOrderId)
        {
            var lstInventory = _context.IventoryProducts
                                                .Include(x => x.MaSachNavigation)
                                                .Include(x => x.User)
                                                .Where(x => x.TrangThaiDuyet != 2 && x.Id == InvenOrderId)
                                                .FirstOrDefault();
            return View(lstInventory);
        }

        [Route("AcceptOrder/{OrderId}")]
        [HttpGet]
        public IActionResult AcceptOrder(int OrderId)
        {
            try
            {
                var Order = _context.IventoryProducts.AsNoTracking().FirstOrDefault(x => x.Id == OrderId);
                var book = _context.Books.FirstOrDefault(x => x.Isbn == Order.MaSach);
                if (Order != null)
                {
                    Order.TrangThaiDuyet = 1;
                    book.Stock += (int)Order.SoLuongNhap;
                    _context.Entry(Order).State = EntityState.Modified;
                    _context.Entry(book).State = EntityState.Modified;
                    _context.SaveChanges();
                    //ViewBag.Message = "Cập nhật đơn nhập hàng thành công!";
                    Console.WriteLine(Order);
                }
                return RedirectToAction("Index", "InventoryManager");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("RejectOrder/{OrderId}")]
        [HttpGet]
        public IActionResult RejectOrder(int OrderId)
        {
            try
            {
                var Order = _context.IventoryProducts.AsNoTracking().FirstOrDefault(x => x.Id == OrderId);
                if (Order != null)
                {
                    Order.TrangThaiDuyet = 3;
                    _context.Entry(Order).State = EntityState.Modified;
                    _context.SaveChanges();
                    //ViewBag.Message = "Cập nhật đơn nhập hàng thành công!";
                    Console.WriteLine(Order);
                }
                return RedirectToAction("Index", "InventoryManager");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("Product")]
        public IActionResult Product()
        {
            var lstInventory = _context.Books
                               .AsNoTracking()
                               .Include(categories => categories.BookCategory)
                               .OrderBy(x => x.Stock)
                               .ToList();
            return View(lstInventory);
        }
    }
}
