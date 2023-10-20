using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.ViewModels.Products.BookItems;
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

    public interface IDataStore<TEntityPost, TEntityForView, TEntityDetailsForView>
    {
        Task<ActionResult<IEnumerable<TEntityForView>>> GetEntities();
        Task<ActionResult<TEntityDetailsForView>> GetEntity(int id);
        Task<IActionResult> PostEntity(TEntityPost entity);
        Task<IActionResult> PutEntity(int id, [FromBody] TEntityPost entity);
        Task<IActionResult> DeleteEntity(int id);
    }
}
