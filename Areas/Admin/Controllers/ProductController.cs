using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly BookStoreContext _context;

        public ProductController(BookStoreContext ctx)
        {
            _context = ctx;
        }

        [Route("")]
        [Route("List")]
        public IActionResult ProductList()
        {
            var lstProduct = _context.Books
                               .AsNoTracking()
                               .Include(categories => categories.BookCategory)
                               .OrderBy(x => x.Isbn)
                               .ToList();
            return View(lstProduct);
        }

        [Route("Filter/categoryId-{categoryId}")]
        [HttpGet]
        public IActionResult FilterProduct(int categoryId)
        {
            var lstProduct = _context.Books
                               .AsNoTracking()
                               .Where(x => x.BookCategoryId == categoryId)
                               .Include(categories => categories.BookCategory)
                               .OrderBy(x => x.Isbn)
                               .ToList();
            return View("ProductList", lstProduct);
        }

        [Route("Edit/productId-{productId}")]
        [HttpGet]
        public IActionResult Edit(int productId)
        {

            var lstProduct = _context.Books
                               .AsNoTracking()
                               .Where(x => x.Isbn == productId)
                               .Include(categories => categories.BookCategory)
                               .Include(author => author.Author)
                               .Include(publisher => publisher.Publisher)
                               .OrderBy(x => x.Isbn)
                               .FirstOrDefault();
            return View(lstProduct);
        }

        [Route("Edit/productId-{productId}")]
        [HttpPost]
        public IActionResult Edit(int productId, IFormCollection values)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Isbn == productId);
            if (bookToUpdate != null)
            {
                // Cập nhật các thuộc tính
                //bookToUpdate.Isbn = values;
                //bookToUpdate.AuthorId = updatedAuthorId;
                //bookToUpdate.PublisherId = updatedPublisherId;
                //bookToUpdate.BookCategoryId = updatedBookCategoryId;
                //bookToUpdate.BookName = updatedBookName;
                //bookToUpdate.Price = updatedPrice;
                //bookToUpdate.PictureThumb = updatedPictureThumb;
                //bookToUpdate.PublishYear = updatedPublishYear;
                //bookToUpdate.Language = updatedLanguage;
                //bookToUpdate.Status = updatedStatus;
                //bookToUpdate.Discount = updatedDiscount;
                //bookToUpdate.TotalPage = updatedTotalPage;
                bookToUpdate.Description = values["Description"];
                //bookToUpdate.Stock = updatedStock;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }

            //var lstProduct = _context.Books
            //                   .AsNoTracking()
            //                   .Where(x => x.Isbn == productId)
            //                   .Include(categories => categories.BookCategory)
            //                   .OrderBy(x => x.Isbn)
            //                   .FirstOrDefault();
            return View(bookToUpdate);
        }


        [Route("Detail/productId-{productId}")]
        [HttpGet]
        public IActionResult ProductDetail(int productId)
        {

            var product = _context.Books
                               .AsNoTracking()
                               .Where(x => x.Isbn == productId)
                               .Include(categories => categories.BookCategory)
                               .Include(author => author.Author)
                               .Include(publisher => publisher.Publisher)
                               .OrderBy(x => x.Isbn)
                               .FirstOrDefault();
            return View(product);
        }
        // Add more actions as needed...
    }
}
