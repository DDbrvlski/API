using BookStoreAPI.Data;
using BookStoreAPI.Interfaces;
using BookStoreAPI.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Helpers.BaseController
{
    public class BaseController<TEntity> : ABaseController<TEntity> where TEntity : BaseEntity
    {
        protected readonly BookStoreContext _context;

        public BaseController(BookStoreContext context)
        {
            _context = context;
        }

        protected override async Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesAsync()
        {
            return await GetAllEntitiesCustomAsync();
        }

        protected override async Task<ActionResult<TEntity>> CreateEntityAsync(TEntity entity)
        {
            await CreateEntityCustomAsync(entity);
            return entity;
        }

        protected override async Task<IActionResult> UpdateEntityAsync(int id, TEntity updatedEntity)
        {

            if (updatedEntity is BaseEntity tempEntity)
            {
                if (id != tempEntity.Id)
                {
                    return BadRequest("Invalid data.");
                }
            }

            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await UpdateEntityCustomAsync(entity, updatedEntity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(updatedEntity);
        }

        protected override async Task<IActionResult> DeleteEntityAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return await DeleteEntityCustomAsync(entity);
        }

        protected override bool EntityExists(int id)
        {
            return _context.Set<TEntity>().Find(id) != null;
        }

        protected override async Task<TEntity?> GetEntityByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        protected override async Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Set<TEntity>().Where(x => x.IsActive == true).ToListAsync();
        }

        protected override async Task CreateEntityCustomAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        protected override async Task UpdateEntityCustomAsync(TEntity oldEntity, TEntity updatedEntity)
        {
            oldEntity.CopyProperties(updatedEntity);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(TEntity entity)
        {
            if (entity is BaseEntity deactivatableEntity)
            {
                deactivatableEntity.IsActive = false;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Nie można zdezaktywować tej encji.");
            }
        }

    }
}

