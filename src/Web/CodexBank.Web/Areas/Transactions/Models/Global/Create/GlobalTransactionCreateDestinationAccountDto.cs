using CodexBank.Common;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Web.Areas.Transactions.Models.Global.Create
{
    public class GlobalTransactionCreateDestinationAccountDto
    {
        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        [Display(Name = "Account/IBAN")]
        public string UniqueId { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        [Display(Name = "Beneficiary's name")]
        public string UserFullName { get; set; }
    }
}
