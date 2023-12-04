﻿using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseController
{
    public abstract class ABaseController<TEntity> : ControllerBase
    {
        protected abstract Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesAsync();
        protected abstract Task<ActionResult<TEntity>> CreateEntityAsync(TEntity entity);
        protected abstract Task<IActionResult> UpdateEntityAsync(int id, TEntity updatedEntity);
        protected abstract Task<IActionResult> DeleteEntityAsync(int id);
        protected abstract bool EntityExists(int id);

        protected virtual async Task<TEntity?> GetEntityByIdAsync(int id) { return default; }
        protected virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesCustomAsync() { return null; }
        protected virtual async Task<IActionResult> CreateEntityCustomAsync(TEntity entity) { return null; }
        protected virtual async Task UpdateEntityCustomAsync(TEntity oldEntity, TEntity updatedEntity) { }
        protected virtual async Task<IActionResult> DeleteEntityCustomAsync(TEntity entity) { return null; }

    }

    public abstract class ABaseController<TEntity, TEntityPost, TEntityForView, TEntityDetailsForView> : ControllerBase
    {
        protected abstract Task<ActionResult<IEnumerable<TEntityForView>>> GetAllEntitiesAsync();
        protected abstract Task<IActionResult> CreateEntityAsync(TEntityPost entity);
        protected abstract Task<IActionResult> UpdateEntityAsync(int id, TEntityPost updatedEntity);
        protected abstract Task<IActionResult> DeleteEntityAsync(int id);
        protected abstract bool EntityExists(int id);

        protected virtual async Task<TEntity?> GetEntityByIdAsync(int id) { return default; }
        protected virtual async Task<ActionResult<TEntityDetailsForView?>> GetCustomEntityByIdAsync(int id) { return default; }
        protected virtual async Task<ActionResult<IEnumerable<TEntityForView>>> GetAllEntitiesCustomAsync() { return null; }
        protected virtual async Task<IActionResult> CreateEntityCustomAsync(TEntityPost entity) { return null; }
        protected virtual async Task UpdateEntityCustomAsync(TEntity oldEntity, TEntityPost updatedEntity) { }
        protected virtual async Task<IActionResult> DeleteEntityCustomAsync(TEntity entity) { return null; }

    }
}
