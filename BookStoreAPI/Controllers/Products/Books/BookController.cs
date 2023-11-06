﻿using BookStoreAPI.BusinessLogic.BookLogic;
using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books;
using BookStoreViewModels.ViewModels.Products.Books.Dictionaries;
using BookStoreViewModels.ViewModels.Products.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers.Products.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : CRUDController<Book, BookPostForView, BookForView, BookDetailsForView>
    {
        public BookController(BookStoreContext context) : base(context)
        {
        }

        
        protected override async Task<BookDetailsForView?> GetCustomEntityByIdAsync(int id)
        {

            return await _context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Where(x => x.Id == id && x.IsActive)
                .Select(element => new BookDetailsForView
                {
                    Id = element.Id,
                    OriginalLanguageName = element.OriginalLanguage.Name,
                    PublisherName = element.Publisher.Name,
                    Description = element.Description,
                    OriginalLanguageID = element.OriginalLanguageID,
                    PublisherID = element.PublisherID,
                    Title = element.Title,
                    Categories = element.BookCategories
                            .Where(z => z.IsActive == true)
                            .Select(y => new CategoryForView
                            {
                                Id = y.Category.Id,
                                Name = y.Category.Name,
                            }).ToList(),
                    Authors = element.BookAuthors
                            .Where(z => z.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList(),
                    Images = element.BookImages
                            .Where(y => y.IsActive == true)
                            .Select(y => new ImagesForView
                            {
                                Id = y.Image.Id,
                                Title = y.Image.Title,
                                ImageURL = y.Image.ImageURL,
                            }).ToList(),
                })
                .FirstAsync();
        }

        protected override async Task<ActionResult<IEnumerable<BookForView>>> GetAllEntitiesCustomAsync()
        {
            return await _context.Book
                .Include(x => x.OriginalLanguage)
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthors)
                    .ThenInclude(x => x.Author)
                .Include(x => x.BookCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.BookImages)
                    .ThenInclude(x => x.Image)
                .Where(x => x.IsActive == true)
                .Select(x => new BookForView
                {
                    Id = x.Id,
                    PublisherName = x.Publisher.Name,
                    Title = x.Title,
                    Authors = x.BookAuthors
                            .Where(y => y.IsActive == true)
                            .Select(y => new AuthorsForView
                            {
                                Id = y.Author.Id,
                                Name = y.Author.Name,
                                Surname = y.Author.Surname,
                            }).ToList()
                })
                .ToListAsync();
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(BookPostForView entity)
        {
            return await BookB.ConvertEntityPostForViewAndSave<BookB>(entity, _context);
        }

        protected override async Task UpdateEntityCustomAsync(Book oldEntity, BookPostForView updatedEntity)
        {
            await BookB.ConvertEntityPostForViewAndUpdate<BookB>(oldEntity, updatedEntity, _context);
        }

        protected override async Task<IActionResult> DeleteEntityCustomAsync(Book entity)
        {
            return await BookB.DeactivateEntityAndSave<BookB>(entity, _context);
        }
    }
}
