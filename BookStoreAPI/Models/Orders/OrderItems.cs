﻿using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.BookItems;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Orders
{
    public class OrderItems : BaseEntity
    {
        public int Quantity { get; set; }

        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        [JsonIgnore]
        public virtual BookItem BookItem { get; set; }

        //Order
        [Required(ErrorMessage = "Zamówienie jest wymagane.")]
        [Display(Name = "Zamówienie")]
        public int? OrderID { get; set; }

        [ForeignKey("OrderID")]
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
