using BookStoreAPI.Data;

namespace BookStoreAPI.Helpers.BaseBusinessLogic
{
    public abstract class ABaseBusinessLogic<TEntity, TEntityPost>
    {
        protected abstract Task ConvertListsToUpdate(TEntity entity, TEntityPost entityWithData, BookStoreContext context);
        protected abstract Task DeactivateAllConnectedEntities(TEntity entity, BookStoreContext context);
    }
}
