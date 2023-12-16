using BookStore.Models;

namespace BookStore.ViewModel
{
    public class CustomerAccountViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Thêm các thuộc tính khác của mô hình Customer mà bạn muốn sử dụng trong trang cshtml
        public int CustomerTypeId { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        // ...

        // Constructor để khởi tạo mô hình từ mô hình Account và Customer
        public CustomerAccountViewModel(Account account, Customer customer)
        {
            Username = account.Username;
            Password = account.Password;
            // Gán các giá trị từ mô hình Customer
            CustomerTypeId = customer.CustomerTypeId;
            Gender = customer.Gender;
            Email = customer.Email;
            FullName = customer.FullName;
            Phone = customer.Phone;
            // ...
        }
        public CustomerAccountViewModel()
        {
            // Có thể để trống hoặc thêm logic khác nếu cần
        }
    }
}
