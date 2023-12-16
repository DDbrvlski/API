using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Data;
using BookStoreData.Models.Accounts;
using BookStoreData.Models.Products.BookItems;
using BookStoreViewModels.ViewModels.Products.BookItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BookStoreAPI.Controllers.Products.BookItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookItemReviewController : CRUDController<BookItemReview, BookReviewPostWWWForView, BookReviewWWWForView, BookReviewPostWWWForView>
    {
        private readonly UserManager<User> _userManager;
        public BookItemReviewController(BookStoreContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Get-Existing-Review")]
        [Authorize(Roles = UserRoles.User)]
        public override async Task<ActionResult<BookReviewPostWWWForView>> GetEntity(int bookItemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            if (customer == null)
            {
                return NotFound("Nie znaleziono danych klienta.");
            }

            var bookReview = await _context.BookItemReview.FirstOrDefaultAsync(x => x.BookItemID == bookItemId);

            if (bookReview == null)
            {
                return null;
            }

            var review = new BookReviewPostWWWForView()
            {
                Content = bookReview.Content,
                BookItemId = bookReview.BookItemID,
                Id = bookReview.Id,
                ScoreId = bookReview.ScoreID
            };

            return review;
        }

        [HttpPost]
        [Route("Add-Review")]
        [Authorize(Roles = UserRoles.User)]
        public override async Task<IActionResult> PostEntity(BookReviewPostWWWForView bookReview)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest("Nie można znaleźć identyfikatora użytkownika.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie można znaleźć użytkownika o podanym identyfikatorze.");
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.IsActive && x.Id == user.CustomerID);

            if (customer == null)
            {
                return NotFound("Nie znaleziono danych klienta.");
            }

            var existingReview = await _context.BookItemReview.FirstOrDefaultAsync(x => x.IsActive && x.CustomerID == customer.Id);

            if (existingReview != null)
            {
                existingReview.Content = bookReview.Content;
                existingReview.ScoreID = bookReview.ScoreId;
            }
            else
            {
                var review = new BookItemReview()
                {
                    Content = bookReview.Content,
                    BookItemID = bookReview.BookItemId,
                    ScoreID = bookReview.ScoreId,
                    CustomerID = customer.Id
                };

                _context.BookItemReview.Add(review);                
            }
            await DatabaseOperationHandler.TryToSaveChangesAsync(_context);

            var listOfScores = await _context.BookItemReview.Where(x => x.IsActive && x.BookItemID == bookReview.BookItemId).Select(x => x.Score.Value).ToListAsync();
            if (!listOfScores.IsNullOrEmpty())
            {
                var scoreToUpdate = await _context.BookItem.FirstAsync(x => x.Id == bookReview.BookItemId);
                scoreToUpdate.Score = listOfScores.Average();
                await Console.Out.WriteLineAsync();
            }

            return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }

        [HttpGet]
        [Route("Get-Product-Reviews")]
        public virtual async Task<ActionResult<IEnumerable<BookReviewWWWForView>>> GetEntities(int bookItemId, int numberOfElements = 4)
        {
            return await _context.BookItemReview
                        .Include(x => x.Customer)
                        .Include(x => x.Score)
                        .Where(x => x.IsActive && x.BookItemID == bookItemId)
                        .Select(x => new BookReviewWWWForView()
                        {
                            Id = x.Id,
                            Content = x.Content,
                            CreationDate = x.CreationDate,
                            CustomerName = x.Customer.Name + " " + x.Customer.Surname,
                            ScoreValue = x.Score.Value
                        }).Take(numberOfElements).ToListAsync();
        }
    }
}
