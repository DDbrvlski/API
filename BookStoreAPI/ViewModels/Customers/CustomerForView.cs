using BookStoreAPI.ViewModels.Helpers;

namespace BookStoreAPI.ViewModels.Customers
{
    public class CustomerForView : BaseView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
