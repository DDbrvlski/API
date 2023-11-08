using BookStoreData.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using BookStoreAPI.BusinessLogic.FooterLinksLogic;

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

        [HttpGet("column-id/{id}")]
        public async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromFooterLinksByColumnId(int id)
        {
            return await GetAllPropertiesFromFooterLinksByColumnIdAsync(id);
        }

        [HttpGet("column-position/{id}")]
        public async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromFooterLinksByColumnPosition(int id)
        {
            return await GetAllPropertiesFromFooterLinksByColumnPositionAsync(id);
        }

        [HttpGet("column-position-order")]
        public async Task<ActionResult<IEnumerable<FooterLinksFV>>> GetAllPropertiesFromFooterLinksInOrderByColumnPosition()
        {
            return await GetAllPropertiesFromFooterLinksInOrderByColumnPositionAsync();
        }

        protected async Task<FooterLinksForView> GetAllPropertiesFromEntityByIdAsync(int id)
        {
             return await _context.FooterLinks
                .Include(x => x.FooterColumn)
                .Where(x => x.Id == id && x.IsActive)
                .Select(element => new FooterLinksForView()
                {
                    Id = id,
                    ColumnId = element.FooterColumn.Id,
                    ColumnName = element.FooterColumn.Name,
                    ColumnPosition = element.FooterColumn.Position,
                    HTMLObject = element.FooterColumn.HTMLObject,
                    Name = element.Name,
                    Path = element.Path,
                    Position = element.Position,
                    URL = element.URL,
                })
                .FirstAsync();
        }

        protected async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromEntitiesAsync()
        {
            return await _context.FooterLinks
                .Include(x => x.FooterColumn)
                .Where(x => x.IsActive == true)
                .Select(x =>  new FooterLinksForView()
                {
                    Id = x.Id,
                    ColumnId = x.FooterColumn.Id,
                    ColumnName = x.FooterColumn.Name,
                    ColumnPosition = x.FooterColumn.Position,
                    HTMLObject = x.FooterColumn.HTMLObject,
                    Name = x.Name,
                    Path = x.Path,
                    Position = x.Position,
                    URL = x.URL,
                })
                .ToListAsync();            
        }

        protected async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromFooterLinksByColumnIdAsync(int id)
        {
            return await _context.FooterLinks
               .Include(x => x.FooterColumn)
               .Where(x => x.IsActive && x.FooterColumn.Id == id)
               .Select(element => new FooterLinksForView()
               {
                   Id = id,
                   ColumnId = element.FooterColumn.Id,
                   ColumnName = element.FooterColumn.Name,
                   ColumnPosition = element.FooterColumn.Position,
                   HTMLObject = element.FooterColumn.HTMLObject,
                   Name = element.Name,
                   Path = element.Path,
                   Position = element.Position,
                   URL = element.URL,
               })
               .ToListAsync();
        }

        protected async Task<ActionResult<IEnumerable<FooterLinksForView>>> GetAllPropertiesFromFooterLinksByColumnPositionAsync(int id)
        {
            return await _context.FooterLinks
               .Include(x => x.FooterColumn)
               .Where(x => x.IsActive && x.FooterColumn.Position == id)
               .Select(element => new FooterLinksForView()
               {
                   Id = id,
                   ColumnId = element.FooterColumn.Id,
                   ColumnName = element.FooterColumn.Name,
                   ColumnPosition = element.FooterColumn.Position,
                   HTMLObject = element.FooterColumn.HTMLObject,
                   Name = element.Name,
                   Path = element.Path,
                   Position = element.Position,
                   URL = element.URL,
               })
               .ToListAsync();
        }
        protected async Task<ActionResult<IEnumerable<FooterLinksFV>>> GetAllPropertiesFromFooterLinksInOrderByColumnPositionAsync()
        {
            var footerLinks = await _context.FooterLinks
                .Include(x => x.FooterColumn)
                .Where(x => x.IsActive == true)
                .Select(x => new FooterLinksForView()
                {
                    Id = x.Id,
                    ColumnId = x.FooterColumn.Id,
                    ColumnName = x.FooterColumn.Name,
                    ColumnPosition = x.FooterColumn.Position,
                    HTMLObject = x.FooterColumn.HTMLObject,
                    Name = x.Name,
                    Path = x.Path,
                    Position = x.Position,
                    URL = x.URL,
                })
            .ToListAsync();

            return footerLinks
            .GroupBy(x => x.ColumnPosition)
            .OrderBy(group => group.Key)
            .Select(group => new FooterLinksFV
            {
                ColumnId = group.First().ColumnId,
                ColumnName = group.First().ColumnName,
                ColumnPosition = group.Key,
                HTMLObject = group.First().HTMLObject,
                FooterLinksList = group
                    .OrderBy(item => item.Position)
                    .Select(item => new FooterLinksListDetailsForView
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Path = item.Path,
                        URL = item.URL,
                        Position = item.Position
                    })
                    .ToList()
            })
            .ToList();
        }
    }
}
