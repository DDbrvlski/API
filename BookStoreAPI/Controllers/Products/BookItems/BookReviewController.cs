using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Products.Books;
using Microsoft.AspNetCore.Mvc;
using BookStoreData.Models.Products.BookItems;
using BookStoreData.Models.Accounts;
using BookStoreViewModels.ViewModels.Products.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BookStoreAPI.Helpers;

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

            var review = new BookItemReview()
            {
                Content = bookReview.Content,
                BookItemID = bookReview.BookItemId,
                ScoreID = bookReview.ScoreId,
                CustomerID = customer.Id
            };

            _context.BookItemReview.Add(review);
            var listOfScores = await _context.BookItemReview.Where(x => x.IsActive && x.BookItemID == bookReview.BookItemId).Select(x => x.Score.Rating).ToListAsync();
            var scoreToUpdate = await _context.BookItem.FirstAsync(x => x.Id == bookReview.BookItemId);
            scoreToUpdate.Score = listOfScores.Average();
            return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }

        [HttpGet]
        [Route("Get-Product-Reviews")]
        public virtual async Task<ActionResult<BookAllReviewsWWWForView>> GetEntities(int bookItemId, int numberOfElements = 4)
        {
            var scoreOccurrences = _context.BookItemReview
                .GroupBy(review => review.Score.Rating)
                .ToDictionary(group => group.Key, group => group.Count());

            var allScores = Enumerable.Range(1, 5).ToDictionary(score => score, score => 0);

            var scoreValues = allScores
                .Concat(scoreOccurrences)
                .GroupBy(x => x.Key)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(pair => pair.Value)
                );

            var bookItemScore = _context.BookItem.FirstAsync(x => x.Id == bookItemId).Result.Score;

            return new BookAllReviewsWWWForView()
            {
                BookItemScore = bookItemScore,
                ScoreValues = scoreValues,
                BookReviews = await _context.BookItemReview
                        .Include(x => x.Customer)
                        .Include(x => x.Score)
                        .Where(x => x.IsActive && x.BookItemID == bookItemId)
                        .Select(x => new BookReviewWWWForView()
                        {
                            Id = x.Id,
                            Content = x.Content,
                            CreationDate = x.CreationDate,
                            CustomerName = x.Customer.Name + " " + x.Customer.Surname,
                            ScoreValue = x.Score.Rating
                        }).Take(numberOfElements).ToListAsync()
            };
        }
    }
}
