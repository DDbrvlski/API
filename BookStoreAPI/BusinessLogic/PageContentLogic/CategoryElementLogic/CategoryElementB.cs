using BookStoreAPI.BusinessLogic.ImageLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.Banner;
using BookStoreViewModels.ViewModels.PageContent.CategoryElements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.PageContentLogic.CategoryElementLogic
{
    public class CategoryElementB : BaseBusinessLogic<CategoryElement, CategoryElementsForView>
    {
        protected override async Task ConvertListsToUpdate(CategoryElement entity, CategoryElementsForView entityWithData, BookStoreContext context)
        {
            if (entity.ImageID != null)
            {
                await ImageManager.DeactivateImage(context, entity.ImageID);
            }
            var image = await ImageManager.AddNewImage(context, entityWithData.ImageTitle, entityWithData.ImageURL);
            entity.ImageID = image.Value.Id;

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
        protected override async Task DeactivateAllConnectedEntities(CategoryElement entity, BookStoreContext context)
        {
            await ImageManager.DeactivateImage(context, entity.ImageID);
        }

        public static async Task<ActionResult<IEnumerable<CategoryElementsForView>>> GetAllCategoryElements(BookStoreContext context)
        {
            return await context.CategoryElement
                .Include(x => x.Image)
                .Where(x => x.IsActive == true)
                .Select(x => new CategoryElementsForView
                {
                    Id = x.Id,
                    Path = x.Path,
                    Content = x.Content,
                    Logo = x.Logo,
                    Position = x.Position,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).ToListAsync();
        }

        public static async Task<ActionResult<CategoryElementsForView?>> GetCategoryElementById(BookStoreContext context, int id)
        {
            return await context.CategoryElement
                .Include(x => x.Image)
                .Where(x => x.IsActive == true && x.Id == id)
                .Select(x => new CategoryElementsForView
                {
                    Id = x.Id,
                    Path = x.Path,
                    Content = x.Content,
                    Logo = x.Logo,
                    Position = x.Position,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).FirstAsync();
        }
    }
}
