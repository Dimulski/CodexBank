using CodexBank.Common;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Models
{
    public class BankUser : IdentityUser
    {
        [Required]
        [MaxLength(ModelConstants.User.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.MiddleNameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.LastNameMaxLength)]
        public string LastName { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
