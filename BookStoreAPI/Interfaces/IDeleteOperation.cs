using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IDeleteOperation<TEntity>
    {
        Task<IActionResult> DeleteEntityAsync(int id);
    }
}
