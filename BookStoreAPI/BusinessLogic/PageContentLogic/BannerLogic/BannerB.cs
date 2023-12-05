using BookStoreAPI.BusinessLogic.ImageLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.Banner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.PageContentLogic.BannerLogic
{
    public class BannerB : BaseBusinessLogic<Banner, BannerForView>
    {
        protected override async Task ConvertListsToUpdate(Banner entity, BannerForView entityWithData, BookStoreContext context)
        {
            if (entity.ImageID != null)
            {
                await ImageManager.DeactivateImage(context, entity.ImageID);
            }
            var image = await ImageManager.AddNewImage(context, entityWithData.ImageTitle, entityWithData.ImageURL);
            entity.ImageID = image.Value.Id;

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
        protected override async Task DeactivateAllConnectedEntities(Banner entity, BookStoreContext context)
        {
            if (entity.ImageID != null)
            {
                await ImageManager.DeactivateImage(context, entity.ImageID);
            }
        }

        public static async Task<ActionResult<IEnumerable<BannerForView>>> GetAllBanners(BookStoreContext context)
        {
            return await context.Banner
                .Include(x => x.Image)
                .Where(x => x.IsActive == true)
                .Select(x => new BannerForView
                {
                    Id = x.Id,
                    Path = x.Path,
                    Title = x.Title,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).ToListAsync();
        }

        public static async Task<ActionResult<BannerForView?>> GetBannerById(BookStoreContext context, int id)
        {
            return await context.Banner
                .Include(x => x.Image)
                .Where(x => x.IsActive == true && x.Id == id)
                .Select(x => new BannerForView
                {
                    Id = x.Id,
                    Path = x.Path,
                    Title = x.Title,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).FirstAsync();
        }
    }
}
