using BookStoreAPI.BusinessLogic.ImageLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.DiscountsBanner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.PageContentLogic.DiscountsBannerLogic
{
    public class DiscountsBannerB : BaseBusinessLogic<DiscountsBanner, DiscountsBannerForView>
    {
        protected override async Task ConvertListsToUpdate(DiscountsBanner entity, DiscountsBannerForView entityWithData, BookStoreContext context)
        {
            if (entity.ImageID != null)
            {
                await ImageManager.DeactivateImage(context, entity.ImageID);
            }
            var image = await ImageManager.AddNewImage(context, entityWithData.ImageTitle, entityWithData.ImageURL);
            entity.ImageID = image.Value.Id;

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
        protected override async Task DeactivateAllConnectedEntities(DiscountsBanner entity, BookStoreContext context)
        {
            if (entity.ImageID != null)
            {
                await ImageManager.DeactivateImage(context, entity.ImageID);
            }
        }

        public static async Task<ActionResult<IEnumerable<DiscountsBannerForView>>> GetAllDiscountsBanners(BookStoreContext context)
        {
            return await context.DiscountsBanner
                .Include(x => x.Image)
                .Where(x => x.IsActive == true)
                .Select(x => new DiscountsBannerForView
                {
                    Id = x.Id,
                    ButtonTitle = x.ButtonTitle,
                    Header = x.Header,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).ToListAsync();
        }

        public static async Task<ActionResult<DiscountsBannerForView?>> GetDiscountsBannerById(BookStoreContext context, int id)
        {
            return await context.DiscountsBanner
                .Include(x => x.Image)
                .Where(x => x.IsActive == true && x.Id == id)
                .Select(x => new DiscountsBannerForView
                {
                    Id = x.Id,
                    ButtonTitle = x.ButtonTitle,
                    Header = x.Header,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).FirstAsync();
        }
    }
}
