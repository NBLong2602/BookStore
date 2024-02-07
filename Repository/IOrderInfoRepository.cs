using BookStore.Models;

namespace BookStore.Repository
{
    public interface IOrderInfoRepository
    {
        IEnumerable<OrderInfo> GetAllOrder();
    }
}
