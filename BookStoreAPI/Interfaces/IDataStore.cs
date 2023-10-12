using BookStoreAPI.Models.Accounts.Dictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IDataStore<T>
    {        
        Task<ActionResult<IEnumerable<T>>> GetEntities();        
        Task<ActionResult<T>> GetEntity(int id);        
        Task<ActionResult<T>> PostEntity(T entity);        
        Task<IActionResult> PutEntity(int id, [FromBody] T entity);        
        Task<IActionResult> DeleteEntity(int id);
    }
}
