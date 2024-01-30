<<<<<<< HEAD
﻿using BookStore.Areas.Admin.ViewModel;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;
=======
﻿using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
>>>>>>> 6734c78b3578962e13a4939b8c582cc3887fc714

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Customer")]
    public class CustomerManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public CustomerManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult CustomerList()
        {
<<<<<<< HEAD
            var result = (from customer in _context.Customers
                          join order in _context.OrderInfos on customer.Id equals order.CustomerId into customerOrders
                          let latestOrder = customerOrders.OrderByDescending(o => o.OrderDate).FirstOrDefault()
                          select new
                          {
                              CustomerId = customer.Id,
                              CustomerTypeId = customer.CustomerTypeId,
                              FullName = customer.FullName,
                              Email = customer.Email,
                              Phone = customer.Phone,
                              TotalPrice = customerOrders.Sum(o => o.TotalPrice),
                              OrderDate = latestOrder != null ? latestOrder.OrderDate : null
                          }).AsEnumerable()
                .Select(x => new CustomerWithTotalPriceViewModel
                {
                    Id = x.CustomerId,
                    CustomerTypeId = x.CustomerTypeId,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    TotalPrice = x.TotalPrice,
                    OrderDate = x.OrderDate,
                    // Các thuộc tính khác của Customer nếu có
                })
                .Where(o => o.CustomerTypeId == 2)
                .OrderByDescending(x => x.TotalPrice)
                .ThenByDescending(x => x.OrderDate);
            return View(result);
=======
            var lstCustomer = _context.Customers
                               .AsNoTracking()
                               .Where(x => x.CustomerTypeId == 2)
                               .OrderBy(x => x.Id)
                               .ToList();
            return View(lstCustomer);
>>>>>>> 6734c78b3578962e13a4939b8c582cc3887fc714
        }

        [Route("Detail")]
        [HttpGet]
        public IActionResult CustomerDetail(int customerId)
        {

<<<<<<< HEAD
            var customer = _context.Customers
=======
            var Customer = _context.Customers
>>>>>>> 6734c78b3578962e13a4939b8c582cc3887fc714
                               .AsNoTracking()
                               .Where(x => x.Id == customerId)
                               .FirstOrDefault();

<<<<<<< HEAD
            return View(customer);
        }

        //[Route("Remove")]
        //[HttpGet]
        //public IActionResult RemoveCustomer(int customerId)
        //{
        //    TempData["Message"] = "";

        //    _context.Remove(_context.Customers.Find(customerId));
        //    _context.SaveChanges();
        //    TempData["Message"] = "Xoa Thanh Cong";
        //    return RedirectToAction("CustomerList", "CustomerManager");

        //}

        //[Route("Add")]
        //[HttpGet]
        //public IActionResult AddCustomer()
        //{
        //    ViewBag.CustomTypeId = new SelectList(_context.CustomerTypes.ToList(), "Id", "Type");
        //    return View();
        //}

        //[Route("Add")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddCustomer(Customer model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Customers.Add(model);
        //        _context.SaveChanges();
        //        ViewBag.SuccessMessage = "Thêm nhân viên thành công!";
        //        return RedirectToAction("CustomerList", "CustomerManager");
        //    }
        //    else
        //    {
        //        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            // In ra console hoặc log để kiểm tra lỗi chi tiết
        //            Console.WriteLine(error.ErrorMessage);
        //        }
        //        return View(model);
        //    }

        //}

        //[Route("Edit")]
        //[HttpGet]
        //public IActionResult EditCustomer(int customerId)
        //{
        //    ViewBag.CustomTypeId = new SelectList(_context.CustomerTypes.ToList(), "Id", "Type");
        //    var customer = _context.Customers.Find(customerId);
        //    return View(customer);
        //}

        //[Route("Edit")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditCustomer(Customer model) // Thay thế BookViewModel bằng tên lớp ViewModel của bạn
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(model).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return RedirectToAction("CustomerList", "CustomerManager");
        //    }
        //    else
        //    {
        //        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            // In ra console hoặc log để kiểm tra lỗi chi tiết
        //            Console.WriteLine(error.ErrorMessage);
        //        }
        //        return View(model);
        //    }

        //}
=======
            return View(Customer);
        }
>>>>>>> 6734c78b3578962e13a4939b8c582cc3887fc714
    }
}
