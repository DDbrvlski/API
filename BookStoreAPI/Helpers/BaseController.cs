using BookStoreAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Helpers
{
    public class BaseController : ControllerBase
    {
        protected readonly BookStoreContext _context;

        public BaseController(BookStoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Finds an element of type TEntity by its ID.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to search for.</typeparam>
        /// <param name="id">The ID of the entity to find.</param>
        /// <returns>The found TEntity or null if not found.</returns>
        protected async Task<TEntity?> EntityExistsAsync<TEntity>(int id) where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}

