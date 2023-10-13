using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IBaseController<T>
    {
        Task<ActionResult<IEnumerable<T>>> GetAllEntities();
        Task<ActionResult<T>> GetEntity(int id);
        Task<ActionResult<T>> CreateEntity(T entity);
        Task<IActionResult> UpdateEntity(int id, [FromBody] T entity);
        Task<IActionResult> DeleteEntity(int id);
    }
}
