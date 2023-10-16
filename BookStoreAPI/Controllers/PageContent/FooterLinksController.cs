using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.BusinessLogic;
using BookStoreAPI.Models.PageContent;
using BookStoreAPI.ViewModels.PageContent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BookStoreAPI.Controllers.PageContent
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterLinksController : CRUDController<FooterLinks>
    {
        public FooterLinksController(BookStoreContext context) : base(context)
        {
        }

        [HttpGet("full-columns")]
        public async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromEntities()
        {
            return await GetAllPropertiesFromEntitiesAsync();
        }

        [HttpGet("full-columns/{id}")]
        public async Task<ActionResult<FooterLinksForView>> GetAllPropertiesFromEntity(int id)
        {
            return await GetAllPropertiesFromEntityByIdAsync(id);
        }

        protected async Task<FooterLinksForView> GetAllPropertiesFromEntityByIdAsync(int id)
        {
            var element = await _context.FooterLinks.Include(x => x.FooterColumn).FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
            return new FooterLinksForView 
            {   
                Id = id,
                ColumnId = element.FooterColumn.Id,
                ColumnName = element.FooterColumn.Name,
                ColumnPosition = element.FooterColumn.Position,
                HTMLObject = element.FooterColumn.HTMLObject
            }.CopyProperties(element);
        }

        protected async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromEntitiesAsync()
        {
            var elements = await _context.FooterLinks.Include(x => x.FooterColumn).ToListAsync();
            return elements.Select(x => new FooterLinksForView 
            { 
                Id = x.Id,
                ColumnId = x.FooterColumn.Id, 
                ColumnName = x.FooterColumn.Name, 
                ColumnPosition = x.FooterColumn.Position, 
                HTMLObject = x.FooterColumn.HTMLObject 
            }.CopyProperties(x)).ToList();
        }

    }
}
