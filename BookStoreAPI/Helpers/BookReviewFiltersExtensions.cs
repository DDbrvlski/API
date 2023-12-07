using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;

namespace BookStoreAPI.Helpers
{
    public static class BookReviewFiltersExtensions
    {
        public static IQueryable<BookItemReview> ApplyReviewFilters(this IQueryable<BookItemReview> query, BookItemsWWWStoreFiltersForView filters)
        {
            if (filters == null)
                return query;


            return query;
        }

        public static IQueryable<BookItem> aw(this IQueryable<BookItem> query, List<int?> authorIds)
        {
            return query.Where(x => x.Book.BookAuthors.Any(a => authorIds.Contains(a.AuthorID)));
        }

    }
}
