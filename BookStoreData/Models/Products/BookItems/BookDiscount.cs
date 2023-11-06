﻿using BookStoreData.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStoreData.Models.Products.BookItems
{
    public class BookDiscount : BaseEntity
    {
        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        [JsonIgnore]
        public virtual BookItem BookItem { get; set; }

        //Discount
        [Required(ErrorMessage = "Przecena jest wymagana.")]
        [Display(Name = "Przecena")]
        public int? DiscountID { get; set; }

        [ForeignKey("DiscountID")]
        [JsonIgnore]
        public virtual Discount Discount { get; set; }
    }
}
