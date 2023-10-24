﻿using BookStoreAPI.Models.Helpers;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Products.BookItems
{
    public class DiscountCode : BaseEntity
    {
        public string Code { get; set; }
        public decimal PercentOfDiscount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime StartingDate { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public List<BookItem>? BookItems { get; set; }
        [JsonIgnore]
        public List<BookDiscountCode>? BookDiscountCodes { get; set; }
    }
}
