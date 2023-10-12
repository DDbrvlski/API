using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces
{
    public interface IUpdateOperation<TEntity>
    {
        Task<IActionResult> UpdateEntityAsync(int id, TEntity updatedEntity);
    }
}
