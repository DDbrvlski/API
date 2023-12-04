using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.PageContent.News
{
    public class NewsDetailsForView : NewsForView
    {
        public string? Content { get; set; }
        public string? AuthorName { get; set; }
    }
}
