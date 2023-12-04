using BookStoreAPI.Helpers;
using BookStoreData.Data;
using BookStoreData.Models.Media;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.BusinessLogic.ImageLogic
{
    public class ImageManager
    {
        public static async Task<ActionResult<Images>> AddNewImage(BookStoreContext context, string Title, string ImageURL)
        {
            Images image = new Images()
            {
                Title = Title, 
                ImageURL = ImageURL 
            };
            context.Images.Add(image);

            await DatabaseOperationHandler.TryToSaveChangesAsync(context);

            return image;
        }

        public static async Task<IActionResult> DeactivateImage(BookStoreContext context, int? imageId)
        {
            var image = context.Images.First(x => x.Id == imageId);
            image.IsActive = false;
            image.ModifiedDate = DateTime.Now;

            return await DatabaseOperationHandler.TryToSaveChangesAsync(context);
        }
    }
}
