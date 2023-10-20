using BookStoreAPI.Data;
using BookStoreAPI.Interfaces;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Helpers;
using BookStoreAPI.ViewModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseController
{
    public class CRUDController<T> : BaseController<T>, IDataStore<T> where T : BaseEntity
    {
        public CRUDController(BookStoreContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetEntities()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetEntity(int id)
        {
            return await GetEntityByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<T>> PostEntity(T entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntity(int id, [FromBody] T entity)
        {
            return await UpdateEntityAsync(id, entity);
        }

    }

    public class CRUDController<TEntity, TEntityPost, TEntityForView, TEntityDetailsForView> : 
        BaseController<TEntity, TEntityPost, TEntityForView, TEntityDetailsForView>,
        IDataStore<TEntityPost, TEntityForView, TEntityDetailsForView>
        where TEntity : BaseEntity
        where TEntityPost : BaseView
    {
        public CRUDController(BookStoreContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntityForView>>> GetEntities()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDetailsForView>> GetEntity(int id)
        {
            return await GetCustomEntityByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostEntity(TEntityPost entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntity(int id, [FromBody] TEntityPost entity)
        {
            return await UpdateEntityAsync(id, entity);
        }
    }
}
