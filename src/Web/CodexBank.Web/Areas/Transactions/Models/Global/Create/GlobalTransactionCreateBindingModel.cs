using CodexBank.Common;
using CodexBank.Common.AutoMapping.Contracts;
using CodexBank.Web.Infrastructure.Helpers.Models;
using CodexBank.Web.Models.BankAccount;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Web.Areas.Transactions.Models.Global.Create
{
    public class GlobalTransactionCreateBindingModel : IMapWith<GlobalTransactionDto>
    {
        [Required]
        public GlobalTransactionCreateDestinationBankDto DestinationBank { get; set; }

        [MaxLength(ModelConstants.Transaction.DescriptionMaxLength)]
        [Display(Name = "Details of Payment")]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Transaction.MinStartingPrice,
            ModelConstants.Transaction.MaxStartingPrice,
            ErrorMessage = NotificationMessages.InvalidTransactionAmount)]
        public decimal Amount { get; set; }

        [Display(Name = "Name")]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string SenderName { get; set; }

        public IEnumerable<OwnBankAccountListingViewModel> OwnAccounts { get; set; }

        [Required]
        [Display(Name = "From Account")]
        public string AccountId { get; set; }
    }
}
