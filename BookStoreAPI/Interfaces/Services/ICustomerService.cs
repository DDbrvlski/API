﻿using BookStoreData.Models.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByEmail(string email);
        Task<IActionResult> SetCustomerNewsletterSubscription(Customer customer);
    }
}
