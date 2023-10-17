﻿using BookStoreAPI.Models.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Products.BookItems
{
    public class RecommendedBooks : BaseEntity
    {
        public int Position { get; set; }

        //BookItem
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookItemID { get; set; }

        [ForeignKey("BookItemID")]
        [JsonIgnore]
        public virtual BookItem BookItem { get; set; }
    }
}
