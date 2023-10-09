using BookStoreAPI.Models.Helpers;
using BookStoreAPI.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using BookStoreAPI.Models.Media;

namespace BookStoreAPI.Models.PageContent
{
    public class FooterLinks : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }
}
