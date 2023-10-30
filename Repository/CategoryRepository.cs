using BookStore.Models;

namespace BookStore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreContext _context;

        public CategoryRepository(BookStoreContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<BookCategory> GetAllCategory()
        {
            return _context.BookCategories;
        }
    }
}
