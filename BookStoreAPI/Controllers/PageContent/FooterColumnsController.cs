using BookStoreAPI.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.PageContent;
using Microsoft.AspNetCore.Http;
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
