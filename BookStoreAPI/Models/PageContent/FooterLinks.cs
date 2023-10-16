using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Media;
using BookStoreAPI.Models.Products.Books.BookDictionaries;
using System.Text.Json.Serialization;

namespace BookStoreAPI.Models.PageContent
{
    public class FooterLinks : DictionaryTable
    {
        public string? Path { get; set; }
        public string? URL { get; set; }
        public int? Position { get; set; }

        //FooterColumn
        [Required(ErrorMessage = "Kolumna jest wymagana.")]
        [Display(Name = "Kolumna")]
        public int? FooterColumnID { get; set; }

        [ForeignKey("FooterColumnID")]
        [JsonIgnore]
        public virtual FooterColumns? FooterColumn { get; set; }
    }
}
