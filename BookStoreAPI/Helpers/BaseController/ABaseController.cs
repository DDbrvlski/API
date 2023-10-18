using BookStoreAPI.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Helpers.BaseController
{
    public abstract class ABaseController<TEntity> : ControllerBase where TEntity : BaseEntity
    {
        protected abstract Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesAsync();
        protected abstract Task<ActionResult<TEntity>> CreateEntityAsync(TEntity entity);
        protected abstract Task<IActionResult> UpdateEntityAsync(int id, TEntity updatedEntity);
        protected abstract Task<IActionResult> DeleteEntityAsync(int id);
        protected abstract bool EntityExists(int id);

        protected virtual async Task<TEntity?> GetEntityByIdAsync(int id) { return null; }
        protected virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesCustomAsync() { return null; }
        protected virtual async Task CreateEntityCustomAsync(TEntity entity) { }
        protected virtual async Task UpdateEntityCustomAsync(TEntity oldEntity, TEntity updatedEntity) { }
        protected virtual async Task<IActionResult> DeleteEntityCustomAsync(TEntity entity) { return null; }

    }
}
