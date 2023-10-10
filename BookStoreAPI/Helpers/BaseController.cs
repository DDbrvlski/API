using BookStoreAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Helpers
{
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        protected readonly BookStoreContext _context;

        public BaseController(BookStoreContext context)
        {
            _context = context;
        }

        protected async Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesAsync()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            return entities;
        }

        protected async Task<ActionResult<TEntity>> CreateEntityAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        protected async Task<IActionResult> UpdateEntityAsync(int id, TEntity updatedEntity)
        {
            //if (id != GetEntityId)
            //{
            //    return BadRequest("Invalid data.");
            //}

            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            // Update entity properties here
            entity.CopyProperties(updatedEntity);

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

        protected async Task<IActionResult> DeleteEntityAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntityExists(int id)
        {
            return (_context.Set<TEntity>().Find(id)) != null;
        }

        protected async Task<TEntity?> GetEntityByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

    }
}

