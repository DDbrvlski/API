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
    public class FooterLinksController : CRUDController<FooterLinksForView>
    {
        public FooterLinksController(BookStoreContext context) : base(context)
        {
        }

        protected override async Task<FooterLinksForView?> GetEntityByIdAsync(int id)
        {
            var element = await _context.FooterLinks.Include(x => x.FooterColumn).FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
            return new FooterLinksForView 
            {   
                ColumnId = element.FooterColumn.Id,
                ColumnName = element.FooterColumn.Name,
                ColumnPosition = element.FooterColumn.Position,
                HTMLObject = element.FooterColumn.HTMLObject
            }.CopyProperties(element);
        }

        protected override async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllEntitiesCustomAsync()
        {
            var elements = await _context.FooterLinks.Include(x => x.FooterColumn).ToListAsync();
            return elements.Select(x => new FooterLinksForView 
            { 
                ColumnId = x.FooterColumn.Id, 
                ColumnName = x.FooterColumn.Name, 
                ColumnPosition = x.FooterColumn.Position, 
                HTMLObject = x.FooterColumn.HTMLObject 
            }.CopyProperties(elements)).ToList();
        }

        protected override async Task CreateEntityCustomAsync(FooterLinksForView entity)
        {
            await FooterLinksB.ConvertFooterLinksForViewToFooterLinksAndSave(entity, _context);
        }
    }
}
