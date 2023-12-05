using BookStoreAPI.Helpers;
using BookStoreAPI.Helpers.BaseController;
using BookStoreAPI.Interfaces.Services;
using BookStoreData.Data;
using BookStoreData.Models.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : CRUDController<Contact>
    {
        private readonly IEmailSenderService _emailSender;
        public ContactController(BookStoreContext context, IEmailSenderService emailSender) : base(context)
        {
            _emailSender = emailSender;
        }

        protected override async Task<IActionResult> CreateEntityCustomAsync(Contact entity)
        {
            _context.Contact.Add(entity);
            await _emailSender.ConfirmationOfContact(entity.ClientName, entity.Email);
            return await DatabaseOperationHandler.TryToSaveChangesAsync(_context);
        }
    }
}
