﻿using BookStoreAPI.Data;
using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;

namespace BookStoreAPI.Helpers.BaseBusinessLogic
{
    public class BaseBusinessLogic<TEntity, TEntityPost> 
        where TEntity : BaseEntity
        where TEntityPost : BaseView
    {
        public static async Task<IActionResult> ConvertEntityPostForViewAndSave<TEntityB>(TEntityPost entityWithData, BookStoreContext context)
            where TEntityB : BaseBusinessLogic<TEntity, TEntityPost>, new()
        {
            return await PerformTransactionAsync(
                async () => await new TEntityB().AddNewEntityAsync(entityWithData, context),
                context
            );
        }

        public static async Task<IActionResult> ConvertEntityPostForViewAndUpdate<TEntityB>(TEntity oldEntity, TEntityPost updatedEntity, BookStoreContext context)
            where TEntityB : BaseBusinessLogic<TEntity, TEntityPost>, new()
        {
            return await PerformTransactionAsync(
                async () => await new TEntityB().UpdateEntityAsync(oldEntity, updatedEntity, context),
                context
            );
        }

        public static async Task<IActionResult> DeactivateEntityAndSave<TEntityB>(TEntity entity, BookStoreContext context)
            where TEntityB : BaseBusinessLogic<TEntity, TEntityPost>, new()
        {
            return await PerformTransactionAsync(
                async () => await new TEntityB().DeactivateEntityAsync(entity, context),
                context
            );
        }

        protected static async Task<IActionResult> PerformTransactionAsync(Func<Task> businessLogicAction, BookStoreContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await businessLogicAction();
                    var result = await TryToSaveChangesAsync(context);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        private static async Task<IActionResult> TryToSaveChangesAsync(BookStoreContext context)
        {
            try
            {
                await context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                Exception innerException = ex.InnerException;

                if (innerException is not null)
                {
                    return new BadRequestObjectResult($"Wewnętrzny wyjątek: {innerException.Message}");
                }
                else
                {
                    return new BadRequestObjectResult($"Wystąpił błąd podczas zapisywania zmian w bazie danych: {ex.Message}");
                }
            }
        }

        protected virtual async Task AddNewEntityAsync(TEntityPost entityWithData, BookStoreContext context)
        {
            TEntity newEntity = Activator.CreateInstance<TEntity>();
            newEntity.CopyProperties(entityWithData);
            context.Set<TEntity>().Add(newEntity);

            await ConvertListsToUpdate(newEntity, entityWithData, context);
        }

        protected virtual async Task UpdateEntityAsync(TEntity oldEntity, TEntityPost updatedEntity, BookStoreContext context)
        {
            oldEntity.CopyProperties(updatedEntity);
            await ConvertListsToUpdate(oldEntity, updatedEntity, context);
        }

        protected virtual async Task DeactivateEntityAsync(TEntity entity, BookStoreContext context)
        {
            entity.IsActive = false;
            await DeactivateAllConnectedEntities(entity, context);
        }

        protected virtual async Task ConvertListsToUpdate(TEntity entity, TEntityPost entityWithData, BookStoreContext context)
        {
            
        }

        protected virtual async Task DeactivateAllConnectedEntities(TEntity entity, BookStoreContext context)
        {
            
        }
    }

}
