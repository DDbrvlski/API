using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.PageContent;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.PageContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterColumnsController : CRUDController<FooterColumns>
    {
        public FooterColumnsController(BookStoreContext context) : base(context)
        {
        }
    }
}
