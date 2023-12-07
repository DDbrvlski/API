using BookStoreData.Models.Orders;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Orders;
using BookStoreViewModels.ViewModels.Products.BookItems;

namespace BookStoreAPI.Helpers
{
    public static class OrderFiltersExtensions
    {
        public static IQueryable<Order> ApplyOrderFilters(this IQueryable<Order> query, OrderFiltersForView filters)
        {
            if (filters == null)
                return query;

            if (filters.OrderStatusId != null)
                query = query.WhereOrderStatus(filters.OrderStatusId);

            return query;
        }

        public static IQueryable<Order> WhereOrderStatus(this IQueryable<Order> query, int? orderStatusId)
        {
            return query.Where(x => x.OrderStatusID == orderStatusId);
        }

    }
}
