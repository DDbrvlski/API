using BookStoreAPI.BusinessLogic.PageContentLogic.BannerLogic;
using BookStoreAPI.BusinessLogic.PageContentLogic.NewsLogic;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.Banner;
using BookStoreViewModels.ViewModels.PageContent.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreAPI.Controllers.PageContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : CRUDController<News, NewsDetailsForView, NewsForView, NewsDetailsForView>
    {
        private readonly UserManager<User> _userManager;
        public NewsController(BookStoreContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Main-News")]
        public async Task<ActionResult<NewsForView>> GetMainNews()
        {
            return await NewsB.GetMainNews(_context);
        }

        [HttpPost]
        //[Authorize(Roles = UserRoles.Employee)]
        public override async Task<IActionResult> PostEntity(NewsDetailsForView entity)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (userId == null)
            //{
            //    return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            //}

            //var user = await _userManager.FindByIdAsync(userId);

            //if (user == null)
            //{
            //    return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            //}

            //var customer = await _context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            //if (customer == null)
            //{
            //    return NotFound("Nie znaleziono danych pracownika.");
            //}
            //entity.AuthorName = customer.Name + " " + customer.Surname;
            entity.AuthorName = "Janusz Dąb";
            return await CreateEntityAsync(entity);
        }

        protected override async Task<ActionResult<NewsDetailsForView?>> GetCustomEntityByIdAsync(int id)
        {
            return await NewsB.GetNewsById(_context, id);
        }

        protected override async Task<ActionResult<IEnumerable<NewsForView>>> GetAllEntitiesCustomAsync()
        {
            return await NewsB.GetAllNews(_context);
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(NewsDetailsForView entity)
        {
            return await NewsB.ConvertEntityPostForViewAndSave<NewsB>(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(News oldEntity, NewsDetailsForView updatedEntity)
        {
            await NewsB.ConvertEntityPostForViewAndUpdate<NewsB>(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(News entity)
        {
            return await NewsB.DeactivateEntityAndSave<NewsB>(entity, _context);
        }
    }
}
