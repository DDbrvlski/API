﻿using BookStoreData.Data;
using BookStoreAPI.Helpers.BaseController;
using BookStoreData.Models.Orders.Dictionaries;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers.Orders.Dictionaries
{
    /// <summary>
    /// Controller for managing delivery methods.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : CRUDController<DeliveryMethod>
    {
        /// <summary>
        /// Initializes a new instance of the DeliveryMethodController.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DeliveryMethodController(BookStoreContext context) : base(context)
        {
        }
    }
}
