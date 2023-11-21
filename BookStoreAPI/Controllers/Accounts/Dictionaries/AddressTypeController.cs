using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.Customers.AddressDictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Accounts.Dictionaries
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypeController : CRUDController<AddressType>
    {
        public AddressTypeController(BookStoreContext context) : base(context)
        {
            
        }
    }
}
