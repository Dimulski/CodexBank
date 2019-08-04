using CodexBank.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Services.Models.BankAccount
{
    public class BankAccountCreateServiceModel : BankAccountBaseServiceModel
    {
        [MaxLength(ModelConstants.BankAccount.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
