﻿using BookStoreAPI.Interfaces;
using BookStoreData.Data;
using BookStoreData.Models.Helpers;
using BookStoreViewModels.ViewModels.Helpers;
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
        BaseController<TEntity, TEntityPost, TEntityForView, TEntityDetailsForView>
        //IDataStore<TEntityPost, TEntityForView, TEntityDetailsForView>
        where TEntity : BaseEntity
        where TEntityPost : BaseView
    {
        public CRUDController(BookStoreContext context) : base(context)
        {
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteEntity(int id)
        {
            return await DeleteEntityAsync(id);
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntityForView>>> GetEntities()
        {
            return await GetAllEntitiesAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntityDetailsForView>> GetEntity(int id)
        {
            return await GetCustomEntityByIdAsync(id);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostEntity(TEntityPost entity)
        {
            return await CreateEntityAsync(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutEntity(int id, [FromBody] TEntityPost entity)
        {
            return await UpdateEntityAsync(id, entity);
        }
    }
}
