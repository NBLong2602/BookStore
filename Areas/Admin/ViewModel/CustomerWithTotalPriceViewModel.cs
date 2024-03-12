using BookStore.Models;
//View/customer/list

namespace BookStore.Areas.Admin.ViewModel
{
    public class CustomerWithTotalPriceViewModel
    {
        public int Id { get; set; }

        public int CustomerTypeId { get; set; }

        public string? FullName { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public float TotalPrice { get; set; }

        public DateTime? OrderDate { get; set; }

        public CustomerWithTotalPriceViewModel(Customer customer, OrderInfo orderInfo) {
            Id = customer.Id;
            CustomerTypeId = customer.CustomerTypeId;
            FullName = customer.FullName;
            Email = customer.Email;
            Phone = customer.Phone;
            TotalPrice = (float)orderInfo.TotalPrice;
            OrderDate = orderInfo.OrderDate;
        }

        public CustomerWithTotalPriceViewModel() { }
    }
}
