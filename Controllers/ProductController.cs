using Azure;
using BookStore.Models;
using BookStore.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using X.PagedList;

namespace BookStore.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly BookStoreContext _context;
        public class PriceRange
        {
            public long Min { get; set; }
            public long Max { get; set; }
        }

        public ProductController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        public IActionResult AllProduct(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstProduct = _context.Books.AsNoTracking().OrderBy(x => x.BookName);
            PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult ProductByCategory(int categoryId, int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstProduct = _context.Books.AsNoTracking().Where(x => x.BookCategoryId == categoryId).OrderBy(x => x.PublishYear);
            PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
            ViewBag.categoryId = categoryId;
            return View(lst);
        }
        public IActionResult ProductDetail(int productId)
        {
            var product = _context.Books.Where(x => x.Isbn == productId).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult Search(string keywords, int? page)
        {
            if (keywords == null)
            {
                return RedirectToAction("AllProduct");
            }
            else
            {
                int pageSize = 12;
                int pageNumber = page == null || page < 0 ? 1 : page.Value;
                var lstProduct = _context.Books.Where(x => x.BookName.Contains(keywords));
                PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
                return View("AllProduct", lst);
            }

        }
        [HttpPost]
        public IActionResult GetFilteredProduct([FromBody] FilterDataProduct filter, int? page)
        {
            //int pageSize = 12;
            //int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstProduct = _context.Books.ToList();
            if (filter.PriceRange != null && filter.PriceRange.Count > 0)
            {
                if (filter.PriceRange.Contains("all"))
                {
                    lstProduct = lstProduct.Where(p => p.Price > 0).ToList();
                }
                else
                {
                    List<PriceRange> priceRanges = new List<PriceRange>();
                    foreach (var range in filter.PriceRange)
                    {
                        var value = range.Split("-").ToArray();
                        PriceRange priceRange = new PriceRange();
                        priceRange.Min = Int64.Parse(value[0]);
                        priceRange.Max = Int64.Parse(value[1]);
                        priceRanges.Add(priceRange);
                    }
                    lstProduct = lstProduct.Where(p => priceRanges.Any(r => p.Price >= r.Min && p.Price <= r.Max)).OrderBy(x=>x.Price).ToList();
                }
            }
            //PagedList<Book> lst = new PagedList<Book>(lstProduct, pageNumber, pageSize);
            return PartialView("_ReturnProductByFilter", lstProduct);
        }

    }
}
