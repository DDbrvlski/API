using BookStoreAPI.Data;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models.PageContent;
using BookStoreAPI.ViewModels.PageContent;

namespace BookStoreAPI.Models.BusinessLogic
{
    public class FooterLinksB
    {
        public static async Task ConvertFooterLinksForViewToFooterLinksAndSave(FooterLinksForView footerLink, BookStoreContext _context)
        {
            var footerLinkToAdd = new FooterLinks().CopyProperties(footerLink);
            _context.FooterLinks.Add(footerLinkToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
