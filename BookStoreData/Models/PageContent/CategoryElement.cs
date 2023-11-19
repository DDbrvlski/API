using BookStoreData.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreData.Models.PageContent
{
    public class CategoryElement : DictionaryTable
    {
        public string Path { get; set; }
        public string Logo { get; set; }
        public string ImgURL { get; set; }
        public string Content { get; set; }
        public int Position { get; set; }
    }
}
