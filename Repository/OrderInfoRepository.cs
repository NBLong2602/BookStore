using BookStore.Models;

namespace BookStore.Repository
{
    public class OrderInfoRepository : IOrderInfoRepository
    {
        private readonly BookStoreContext _context;

        public OrderInfoRepository(BookStoreContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<OrderInfo> GetAllOrder()
        {
            return _context.OrderInfos;
        }
    }
}
