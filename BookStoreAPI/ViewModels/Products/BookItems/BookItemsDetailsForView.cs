﻿using BookStoreAPI.ViewModels.Helpers;
using BookStoreAPI.ViewModels.Products.Books.Dictionaries;

namespace BookStoreAPI.ViewModels.Products.BookItems
{
    public class BookItemsDetailsForView : BaseView
    {
        public decimal BruttoPrice { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public DateTime PublishingDate { get; set; }
        public string TranslatorName { get; set; }
        public string LanguageName { get; set; }
        public string BookName { get; set; }
        public string EditionName { get; set; }
        public string FileFormatName { get; set; }
        public string FormName { get; set; }
        public string AvailabilityName { get; set; }
        public int BookId { get; set; }
    }
}
