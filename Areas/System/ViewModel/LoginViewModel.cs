using BookStore.Models;

namespace BookStore.Areas.System.ViewModel
{
    public class LoginViewModel
    {
        public int role {  get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public LoginViewModel(CustomerType customerType, Account account)
        {
            role = customerType.Id;
            userName = account.Username;
            password = account.Password;
        }

        public LoginViewModel() { }
    }
}
