using BookStoreData.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IBaseController<TEntity> where TEntity : BaseEntity
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllEntitiesAsync();
        Task<ActionResult<TEntity>> CreateEntityAsync(TEntity entity);
        Task<IActionResult> UpdateEntityAsync(int id, TEntity updatedEntity);
        Task<IActionResult> DeleteEntityAsync(int id);
    }
}
