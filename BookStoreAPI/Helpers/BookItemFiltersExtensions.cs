using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;

namespace BookStoreAPI.Helpers
{
    public static class BookItemFiltersExtensions
    {
        public static IQueryable<BookItem> ApplyBookFilters(this IQueryable<BookItem> query, BookItemsWWWStoreFiltersForView filters)
        {
            if (filters == null)
                return query;

            if (!string.IsNullOrEmpty(filters.searchPhrase))
                query = query.SearchBy(filters.searchPhrase);

            if (filters.authorIds != null && filters.authorIds.Any())
                query = query.WhereHasAuthors(filters.authorIds);

            if (filters.categoryIds != null && filters.categoryIds.Any())
                query = query.WhereHasCategories(filters.categoryIds);

            if (filters.formIds != null && filters.formIds.Any())
                query = query.WhereHasForms(filters.formIds);

            if (filters.publisherIds != null && filters.publisherIds.Any())
                query = query.WhereHasPublishers(filters.publisherIds);

            if (filters.languageIds != null && filters.languageIds.Any())
                query = query.WhereHasLanguages(filters.languageIds);

            if (filters.scoreValues != null && filters.scoreValues.Any())
                query = query.WhereHasScores(filters.scoreValues);

            if (filters.availabilitiesIds != null && filters.availabilitiesIds.Any())
                query = query.WhereHasAvailabilities(filters.availabilitiesIds);

            if (filters.priceFrom != null)
                query = query.WherePriceFrom(filters.priceFrom);

            if (filters.priceTo != null)
                query = query.WherePriceTo(filters.priceTo);

            if (filters.isOnSale != null)
                query = query.WhereIsOnSale(filters.isOnSale);

            if (filters.numberOfElements != null)
                query = query.WhereNumberOfElements(filters.numberOfElements);

            if (!string.IsNullOrEmpty(filters.sortBy) && !string.IsNullOrEmpty(filters.sortOrder))
                query = query.OrderBy(filters.sortBy, filters.sortOrder);

            return query;
        }

        public static IQueryable<BookItem> WhereHasAuthors(this IQueryable<BookItem> query, List<int?> authorIds)
        {
            return query.Where(x => x.Book.BookAuthors.Any(a => authorIds.Contains(a.AuthorID)));
        }

        public static IQueryable<BookItem> WhereHasCategories(this IQueryable<BookItem> query, List<int?> categoryIds)
        {
            return query.Where(x => x.Book.BookCategories.Any(a => categoryIds.Contains(a.CategoryID)));
        }

        public static IQueryable<BookItem> WhereHasForms(this IQueryable<BookItem> query, List<int?> formIds)
        {
            return query.Where(x => formIds.Contains(x.FormID));
        }

        public static IQueryable<BookItem> WhereHasPublishers(this IQueryable<BookItem> query, List<int?> publisherIds)
        {
            return query.Where(x => publisherIds.Contains(x.Book.PublisherID));
        }

        public static IQueryable<BookItem> WhereHasLanguages(this IQueryable<BookItem> query, List<int?> languageIds)
        {
            return query.Where(x => languageIds.Contains(x.Book.OriginalLanguageID));
        }

        public static IQueryable<BookItem> WhereHasScores(this IQueryable<BookItem> query, List<int?> scoreValues)
        {
            return query.Where(x => scoreValues.Contains((int)x.Score));
        }

        public static IQueryable<BookItem> WhereHasAvailabilities(this IQueryable<BookItem> query, List<int?> availabilitiesIds)
        {
            return query.Where(x => availabilitiesIds.Contains(x.AvailabilityID));
        }

        public static IQueryable<BookItem> WherePriceFrom(this IQueryable<BookItem> query, decimal? priceFrom)
        {
            return query.Where(x => (x.NettoPrice * (1 + (decimal)x.VAT / 100)) >= priceFrom);
        }

        public static IQueryable<BookItem> WherePriceTo(this IQueryable<BookItem> query, decimal? priceTo)
        {
            return query.Where(x => (x.NettoPrice * (1 + (decimal)x.VAT / 100)) <= priceTo);
        }

        public static IQueryable<BookItem> WhereIsOnSale(this IQueryable<BookItem> query, bool? isOnSale)
        {
            return isOnSale.HasValue && isOnSale.Value
                ? query.Where(x => x.BookDiscounts.Any(y => y.BookItemID == x.Id))
                : query;
        }

        public static IQueryable<BookItem> OrderBy(this IQueryable<BookItem> query, string sortBy, string sortOrder)
        {
            return sortBy.ToLower() switch
            {
                "popular" => sortOrder.ToLower() == "asc" ? query.OrderBy(x => x.SoldUnits) : query.OrderByDescending(x => x.SoldUnits),
                "price" => sortOrder.ToLower() == "asc" ? query.OrderBy(x => x.NettoPrice * (1 + (decimal)x.VAT / 100)) : query.OrderByDescending(x => x.NettoPrice * (1 + (decimal)x.VAT / 100)),
                "alphabetical" => sortOrder.ToLower() == "asc" ? query.OrderBy(x => x.Book.Title) : query.OrderByDescending(x => x.Book.Title),
                "recentlyAdded" => sortOrder.ToLower() == "asc" ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id),
                _ => query
            };
        }

        public static IQueryable<BookItem> SearchBy(this IQueryable<BookItem> query, string searchPhrase)
        {
            return query.Where(x =>
                x.Book.Title.Contains(searchPhrase) ||
                x.Book.BookAuthors.Any(a => a.Author.Name.Contains(searchPhrase) || a.Author.Surname.Contains(searchPhrase)) || 
                x.Book.BookCategories.Any(c => c.Category.Name.Contains(searchPhrase)));
        }

        public static IQueryable<BookItem> WhereNumberOfElements(this IQueryable<BookItem> query, int? numberOfElements)
        {
            return query.Take((int)numberOfElements);
        }
    }

}
