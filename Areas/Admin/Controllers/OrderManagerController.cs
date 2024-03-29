﻿using BookStore.Areas.Admin.ViewModel;
using BookStore.Areas.Models.Authentication;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Manager/Order")]
    [Authentication]
    [AdminAuthorize]
    public class OrderManagerController : Controller
    {
        private readonly BookStoreContext _context;

        public OrderManagerController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult OrderList()
        {
            var lstOrder = _context.OrderInfos.OrderByDescending(x=>x.OrderDate).ToList();
            return View(lstOrder);
        }
    }
}
