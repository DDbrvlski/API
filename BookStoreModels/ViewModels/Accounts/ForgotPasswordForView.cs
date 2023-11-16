using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreViewModels.ViewModels.Accounts
{
    public class ForgotPasswordForView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
