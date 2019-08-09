using CodexBank.Common;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Web.Areas.Transactions.Models.Global.Create
{
    public class GlobalTransactionCreateDestinationBankDto
    {
        [Required]
        [MaxLength(ModelConstants.BankAccount.SwiftCodeMaxLength)]
        [Display(Name = "Swift/Bank code")]
        public string SwiftCode { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.CountryMaxLength)]
        public string Country { get; set; }

        [Required]
        public GlobalTransactionCreateDestinationAccountDto Account { get; set; }
    }
}
