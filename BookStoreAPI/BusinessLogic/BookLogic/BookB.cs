using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.BusinessLogic.BookLogic
{
    public class BookB : BaseBusinessLogic<Book, BookPostForView>
    {
        protected override async Task ConvertListsToUpdate(Book entity, BookPostForView entityWithData, BookStoreContext context)
        {
            List<int?> authorIds = entityWithData.ListOfBookAuthors.Select(x => x.Id).ToList();
            List<int?> categoryIds = entityWithData.ListOfBookCategories.Select(x => x.Id).ToList();
            List<ImagesForView> images = entityWithData.ListOfBookImages.ToList();

            await UpdateAllConnectedEntitiesLists(entity, authorIds, categoryIds, images, context);
        }

        protected override async Task DeactivateAllConnectedEntities(Book entity, BookStoreContext context)
        {
            await BookAuthorManager.DeactivateAllAuthors(entity, context);
            await BookCategoryManager.DeactivateAllCategories(entity, context);
            await BookImageManager.DeactivateAllImages(entity, context);
        }

        private static async Task UpdateAllConnectedEntitiesLists(Book book, List<int?> authorIds, List<int?> categoryIds, List<ImagesForView?> images, BookStoreContext _context)
        {
            await BookAuthorManager.UpdateAuthors(book, authorIds, _context);
            await BookCategoryManager.UpdateCategories(book, categoryIds, _context);
            await BookImageManager.UpdateImages(book, images, _context);
        }
    }
}
