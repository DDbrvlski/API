using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IReadOperation<TEntity>
    {
        Task<TEntity?> GetEntityByIdAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesAsync();
    }
}
