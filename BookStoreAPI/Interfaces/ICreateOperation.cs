using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface ICreateOperation<TEntity>
    {
        Task<ActionResult<TEntity>> CreateEntityAsync(TEntity entity);
    }
}
