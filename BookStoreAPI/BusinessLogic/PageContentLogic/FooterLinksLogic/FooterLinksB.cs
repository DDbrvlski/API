using BookStoreData.Data;
using BookStoreAPI.Helpers;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.FooterLinks;

namespace BookStoreAPI.BusinessLogic.PageContentLogic.FooterLinksLogic
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
