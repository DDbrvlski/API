using BookStoreAPI.Models.Accounts;
using BookStoreAPI.Models.Accounts.Dictionaries;
using BookStoreAPI.Models.Basket;
using BookStoreAPI.Models.Customers;
using BookStoreAPI.Models.Customers.AddressDictionaries;
using BookStoreAPI.Models.Customers.CustomerDictionaries;
using BookStoreAPI.Models.Delivery;
using BookStoreAPI.Models.Delivery.Dictionaries;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Notifications;
using BookStoreAPI.Models.Orders;
using BookStoreAPI.Models.Orders.Dictionaries;
using BookStoreAPI.Models.PageContent;
using BookStoreAPI.Models.Products.BookItems;
using BookStoreAPI.Models.Products.BookItems.BookItemDictionaries;
using BookStoreAPI.Models.Products.Books;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using BookStoreAPI.Models.Rental;
using BookStoreAPI.Models.Rentals.Dictionaries;
using BookStoreAPI.Models.Supplies;
using BookStoreAPI.Models.Supplies.Dictionaries;
using BookStoreAPI.Models.Supply;
using BookStoreAPI.Models.Transactions;
using BookStoreAPI.Models.Transactions.Dictionaries;
using BookStoreAPI.Models.Wishlist;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }


        //Accounts
        public DbSet<AccountStatus> AccountStatus { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
        public DbSet<User> User { get; set; }

        //Basket
        public DbSet<BasketItem> BasketItem { get; set; }

        //Customers
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }

        //Delivery
        public DbSet<ShippingStatus> ShippingStatus { get; set; }
        public DbSet<Shipping> Shipping { get; set; }

        //Media
        public DbSet<Images> Images { get; set; }

        //Notifications
        public DbSet<Newsletter> Newsletter { get; set; }

        //Orders
        public DbSet<DeliveryMethod> DeliveryMethod { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Order> Order { get; set; }

        //PageContent
        public DbSet<FooterLinks> FooterLinks { get; set; }
        public DbSet<News> News { get; set; }

        //Products
        //BookItems
        public DbSet<Availability> Availability { get; set; }
        public DbSet<Edition> Edition { get; set; }
        public DbSet<FileFormat> FileFormat { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<BookDiscount> BookDiscount { get; set; }
        public DbSet<BookItem> BookItem { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<RecommendedBooks> RecommendedBooks { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<StockAmount> StockAmount { get; set; }
        public DbSet<UserRecommendedBooks> UserRecommendedBooks { get; set; }
        //Books
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Translator> Translator { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
        public DbSet<BookReview> BookReview { get; set; }

        //Rentals
        public DbSet<RentalStatus> RentalStatus { get; set; }
        public DbSet<RentalType> RentalType { get; set; }
        public DbSet<Rental> Rental { get; set; }

        //Supplies
        public DbSet<DeliveryStatus> DeliveryStatus { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Supply> Supply { get; set; }
        public DbSet<SupplyGoods> SupplyGoods { get; set; }

        //Transactions
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<TransactionsStatus> TransactionsStatus { get; set; }
        public DbSet<Payment> Payment { get; set; }

        //Wishlist
        public DbSet<WishlistItem> WishlistItem { get; set; }

    }
}
