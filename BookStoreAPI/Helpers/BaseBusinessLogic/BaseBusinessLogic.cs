using BookStoreData.Data;
using BookStoreData.Models.Helpers;
using BookStoreViewModels.ViewModels.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Helpers.BaseBusinessLogic
{
    /// <summary>
    /// Klasa bazowa dla logiki biznesowej obsługująca operacje CRUD na encjach.
    /// </summary>
    /// <typeparam name="TEntity">Typ encji bazodanowej.</typeparam>
    /// <typeparam name="TEntityPost">Typ obiektu wykorzystywanego do zapisu lub aktualizacji encji.</typeparam>
    public class BaseBusinessLogic<TEntity, TEntityPost>
        where TEntity : BaseEntity
        where TEntityPost : BaseView
    {
        /// <summary>
        /// Konwertuje obiekt <typeparamref name="TEntityPost"/> na encję, zapisuje ją w bazie danych i zwraca wynik operacji.
        /// </summary>
        /// <typeparam name="TEntityB">Typ klasy dziedziczącej z <see cref="BaseBusinessLogic{TEntity, TEntityPost}"/>.</typeparam>
        /// <param name="entityWithData">Obiekt zawierający dane do zapisu.</param>
        public static async Task<IActionResult> ConvertEntityPostForViewAndSave<TEntityB>(TEntityPost entityWithData, BookStoreContext context)
            where TEntityB : BaseBusinessLogic<TEntity, TEntityPost>, new()
        {
            return await PerformTransactionAsync(
                async () => await new TEntityB().AddNewEntityAsync(entityWithData, context),
                "dodawania",
                context
            );
        }

        /// <summary>
        /// Konwertuje obiekt <typeparamref name="TEntityPost"/> na istniejącą encję, aktualizuje ją w bazie danych i zwraca wynik operacji.
        /// </summary>
        /// <typeparam name="TEntityB">Typ klasy dziedziczącej z <see cref="BaseBusinessLogic{TEntity, TEntityPost}"/>.</typeparam>
        /// <param name="oldEntity">Stara encja, która ma zostać zaktualizowana.</param>
        /// <param name="updatedEntity">Obiekt zawierający zaktualizowane dane.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        /// <returns>Wynik operacji, np. <see cref="OkResult"/> lub <see cref="BadRequestObjectResult"/>.</returns>
        public static async Task<IActionResult> ConvertEntityPostForViewAndUpdate<TEntityB>(TEntity oldEntity, TEntityPost updatedEntity, BookStoreContext context)
            where TEntityB : BaseBusinessLogic<TEntity, TEntityPost>, new()
        {
            return await PerformTransactionAsync(
                async () => await new TEntityB().UpdateEntityAsync(oldEntity, updatedEntity, context),
                "aktualizowania",
                context
            );
        }

        /// <summary>
        /// Deaktywuje istniejącą encję i zapisuje zmiany w bazie danych.
        /// </summary>
        /// <typeparam name="TEntityB">Typ klasy dziedziczącej z <see cref="BaseBusinessLogic{TEntity, TEntityPost}"/>.</typeparam>
        /// <param name="entity">Encja, która ma zostać zdeaktywowana.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        /// <returns>Wynik operacji, np. <see cref="OkResult"/> lub <see cref="BadRequestObjectResult"/>.</returns>
        public static async Task<IActionResult> DeactivateEntityAndSave<TEntityB>(TEntity entity, BookStoreContext context)
            where TEntityB : BaseBusinessLogic<TEntity, TEntityPost>, new()
        {
            return await PerformTransactionAsync(
                async () => await new TEntityB().DeactivateEntityAsync(entity, context),
                "usuwania",
                context
            );
        }

        /// <summary>
        /// Wykonuje operację biznesową w ramach transakcji bazy danych i zwraca wynik operacji.
        /// </summary>
        /// <param name="businessLogicAction">Akcja zawierająca operację biznesową do wykonania.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        /// <returns>Wynik operacji, np. <see cref="OkResult"/> lub <see cref="BadRequestObjectResult"/>.</returns>
        protected static async Task<IActionResult> PerformTransactionAsync(Func<Task> businessLogicAction, string operationName, BookStoreContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await DatabaseOperationHandler.HandleDatabaseOperation(
                        async () => await businessLogicAction(),
                        operationName
                    );
                    var result = await DatabaseOperationHandler.TryToSaveChangesAsync(context);
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

        /// <summary>
        /// Dodaje nową encję na podstawie obiektu <typeparamref name="TEntityPost"/>.
        /// </summary>
        /// <param name="entityWithData">Obiekt zawierający dane nowej encji.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        protected virtual async Task AddNewEntityAsync(TEntityPost entityWithData, BookStoreContext context)
        {
            TEntity newEntity = Activator.CreateInstance<TEntity>();
            newEntity.CopyProperties(entityWithData);
            context.Set<TEntity>().Add(newEntity);

            await ConvertListsToUpdate(newEntity, entityWithData, context);
        }

        /// <summary>
        /// Aktualizuje istniejącą encję na podstawie obiektu <typeparamref name="TEntityPost"/>.
        /// </summary>
        /// <param name="oldEntity">Stara encja, która ma zostać zaktualizowana.</param>
        /// <param name="updatedEntity">Obiekt zawierający zaktualizowane dane.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        protected virtual async Task UpdateEntityAsync(TEntity oldEntity, TEntityPost updatedEntity, BookStoreContext context)
        {
            oldEntity.CopyProperties(updatedEntity);
            await ConvertListsToUpdate(oldEntity, updatedEntity, context);
        }

        /// <summary>
        /// Deaktywuje encję ustawiając właściwość <see cref="BaseEntity.IsActive"/> na <see langword="false"/>.
        /// </summary>
        /// <param name="entity">Encja, która ma zostać zdeaktywowana.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        protected virtual async Task DeactivateEntityAsync(TEntity entity, BookStoreContext context)
        {
            entity.IsActive = false;
            await DeactivateAllConnectedEntities(entity, context);
        }

        /// <summary>
        /// Konwertuje listy lub kolekcje powiązane z encją w celu zaktualizowania.
        /// </summary>
        /// <param name="entity">Encja, którą należy zaktualizować.</param>
        /// <param name="entityWithData">Obiekt zawierający dane do aktualizacji.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        protected virtual async Task ConvertListsToUpdate(TEntity entity, TEntityPost entityWithData, BookStoreContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deaktywuje wszystkie powiązane encje.
        /// </summary>
        /// <param name="entity">Encja, dla której należy deaktywować powiązane encje.</param>
        /// <param name="context">Kontekst bazy danych.</param>
        protected virtual async Task DeactivateAllConnectedEntities(TEntity entity, BookStoreContext context)
        {
            throw new NotImplementedException();
        }
    }
}