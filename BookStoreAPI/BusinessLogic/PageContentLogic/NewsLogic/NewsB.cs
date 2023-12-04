using BookStoreAPI.BusinessLogic.ImageLogic;
using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseBusinessLogic;
using BookStoreData.Data;
using BookStoreData.Models.PageContent;
using BookStoreViewModels.ViewModels.PageContent.Banner;
using BookStoreViewModels.ViewModels.PageContent.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.BusinessLogic.PageContentLogic.NewsLogic
{
    public class NewsB : BaseBusinessLogic<News, NewsDetailsForView>
    {
        protected override async Task ConvertListsToUpdate(News entity, NewsDetailsForView entityWithData, BookStoreContext context)
        {
            if (entity.ImageID != null)
            {
                await ImageManager.DeactivateImage(context, entity.ImageID);
            }
            var image = await ImageManager.AddNewImage(context, entityWithData.ImageTitle, entityWithData.ImageURL);
            entity.ImageID = image.Value.Id;

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
        protected override async Task DeactivateAllConnectedEntities(News entity, BookStoreContext context)
        {
            await ImageManager.DeactivateImage(context, entity.ImageID);
        }

        public static async Task<ActionResult<IEnumerable<NewsForView>>> GetAllNews(BookStoreContext context)
        {
            return await context.News
                .Include(x => x.Image)
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.Id)
                .Select(x => new NewsForView
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).Take(12).ToListAsync();
        }

        public static async Task<ActionResult<NewsDetailsForView?>> GetNewsById(BookStoreContext context, int id)
        {
            return await context.News
                .Include(x => x.Image)
                .Where(x => x.IsActive == true && x.Id == id)
                .Select(x => new NewsDetailsForView
                {
                    Id = x.Id,
                    Content = x.Content,
                    Topic = x.Topic,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).FirstAsync();
        }

        public static async Task<ActionResult<NewsForView>> GetMainNews(BookStoreContext context)
        {
            return await context.News
                .Include(x => x.Image)
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.Id)
                .Select(x => new NewsDetailsForView
                {
                    Id = x.Id,
                    Content = x.Content,
                    Topic = x.Topic,
                    ImageTitle = x.Image.Title,
                    ImageURL = x.Image.ImageURL
                }).FirstAsync();
        }
    }
}
