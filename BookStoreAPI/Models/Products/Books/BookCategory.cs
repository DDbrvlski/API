﻿using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.Products.Books
{
    public class BookCategory : BaseEntity
    {
        //Category
        [Required(ErrorMessage = "Kategoria jest wymagana.")]
        [Display(Name = "Kategoria")]
        public int? CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [JsonIgnore]
        public virtual Category Category { get; set; }

        //Book
        [Required(ErrorMessage = "Książka jest wymagana.")]
        [Display(Name = "Książka")]
        public int? BookID { get; set; }

        [ForeignKey("BookID")]
        [JsonIgnore]
        public virtual Book Book { get; set; }
    }
}
