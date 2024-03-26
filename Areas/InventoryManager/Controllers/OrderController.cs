using BookStore.Areas.Models.Authentication;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.InventoryManager.Controllers
{
    [Area("InventoryManager")]
    [Route("Inventory")]
    [Route("Inventory/Order")]
    [Authentication]
    [InventoryAuthorize]
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("Add")]
        [HttpGet]
        public IActionResult AddOrder()
        {
            ViewBag.MaSach = new SelectList(_context.Books.ToList(), "Isbn", "BookName");
            return View();
        }

        [Route("Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrder(IventoryProduct model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = int.Parse(HttpContext.Session.GetString("AccountId"));
                model.TrangThaiDuyet = 0;
                _context.IventoryProducts.Add(model);
                _context.SaveChanges();
                ViewBag.SuccessMessage = "Thêm đơn nhập thành công!";
                return RedirectToAction("Index", "HomeInventory");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // In ra console hoặc log để kiểm tra lỗi chi tiết
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }
        }

        [Route("CancelById/{khoId}")]
        [HttpGet]
        public IActionResult CancelById(int khoId)
        {
            try
            {
            var Order = _context.IventoryProducts.AsNoTracking().FirstOrDefault(x => x.Id == khoId);
            if (Order != null)
            {
                Order.TrangThaiDuyet = 2;
                _context.Entry(Order).State = EntityState.Modified;
                _context.SaveChanges();
                //ViewBag.Message = "Cập nhật đơn nhập hàng thành công!";
                Console.WriteLine(Order);
            }
            return RedirectToAction("Index", "HomeInventory");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("UpdateOrderById")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderById(IventoryProduct model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var Order = _context.IventoryProducts.AsNoTracking().FirstOrDefault(x => x.Id == model.Id);
                    if(Order != null) //Update
                    {
                        var updateData = new IventoryProduct
                        {
                            Id = model.Id,
                            MaSach = model.MaSach,
                            SoLuongNhap = model.SoLuongNhap,
                            DonGiaNhap = model.DonGiaNhap,
                            Vat = model.Vat,
                            ThanhTien = model.SoLuongNhap * model.DonGiaNhap * (1 + model.Vat / 100),
                            NgayNhap = model.NgayNhap,
                            TrangThaiDuyet = 0,
                            UserId = int.Parse(HttpContext.Session.GetString("AccountId")),
                    };
                        _context.Entry(updateData).State = EntityState.Modified;
                        _context.SaveChanges();
                        ViewBag.Message = "Cập nhật đơn nhập hàng thành công!";
                    }
                    else //Error
                    {
                        ViewBag.Message = "Đơn hàng không tồn tại!";
                    }
                    return RedirectToAction("Index", "HomeInventory");
                }
                else 
                {
                    ViewBag.Message = "Hành động thất bại!";
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
