using BookStore.Models;

namespace BookStore.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<BookCategory> GetAllCategory();
    }
}
