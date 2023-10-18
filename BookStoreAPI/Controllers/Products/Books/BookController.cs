using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBookController;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Models.BusinessLogic;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.PageContent;
using BookStoreAPI.ViewModels.Products.Books;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CRUDBookController
    {
        public BookController(BookStoreContext context) : base(context)
        {
        }

    }
}
