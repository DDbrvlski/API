using BookStoreData.Data;
using BookStoreData.Models.Helpers;
using BookStoreViewModels.ViewModels.Helpers;
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

            return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
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
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id && x.IsActive) != null;
        }

        protected override async Task<TEntity?> GetEntityByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }
        protected override async Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Set<TEntity>().Where(x => x.IsActive == true).ToListAsync();
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
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
                return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
            }
            else
            {
                return BadRequest("Nie można zdezaktywować tej encji.");
            }
        }

    }

    public class BaseController<TEntity, TEntityPost, TEntityForView, TEntityDetailsForView> : ABaseController<TEntity, TEntityPost, TEntityForView, TEntityDetailsForView>
        where TEntity : BaseEntity
        where TEntityPost : BaseView
    {
        protected readonly BookStoreContext _context;

        public BaseController(BookStoreContext context)
        {
            _context = context;
        }

        protected override async Task<ActionResult<IEnumerable<TEntityForView>>> GetAllEntitiesAsync()
        {
            return await GetAllEntitiesCustomAsync();
        }
        protected override async Task<IActionResult> CreateEntityAsync(TEntityPost entity)
        {
            return await CreateEntityCustomAsync(entity);
        }
        protected override async Task<IActionResult> UpdateEntityAsync(int id, TEntityPost updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Invalid data.");
            }

            var entity = await GetEntityByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await UpdateEntityCustomAsync(entity, updatedEntity);

            return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
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
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id && x.IsActive) != null;
        }

        protected override async Task<TEntity?> GetEntityByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }
        protected override async Task<IActionResult> CreateEntityCustomAsync(TEntityPost entity)
        {
            try
            {
                TEntity newEntity = Activator.CreateInstance<TEntity>();
                newEntity.CopyProperties(entity);
                _context.Set<TEntity>().Add(newEntity);
                return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
            }
            catch (Exception ex)
            {
                return BadRequest("Wystąpił błąd.");
            }
        }
        protected override async Task UpdateEntityCustomAsync(TEntity oldEntity, TEntityPost updatedEntity)
        {
            oldEntity.CopyProperties(updatedEntity);
        }
        protected override async Task<IActionResult> DeleteEntityCustomAsync(TEntity entity)
        {
            if (entity is BaseEntity deactivatableEntity)
            {
                deactivatableEntity.IsActive = false;
                return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
            }
            else
            {
                return BadRequest("Nie można zdezaktywować tej encji.");
            }
        }

    }
}

