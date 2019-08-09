using CodexBank.Common;
using CodexBank.Common.AutoMapping.Contracts;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.Transaction;
using CodexBank.Web.Models.BankAccount;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace CodexBank.Web.Areas.Transactions.Models.Internal
{
    public class InternalTransactionCreateBindingModel : IMapWith<TransactionCreateServiceModel>, IValidatableObject
    {
        private const string DestinationAccountIncorrectError =
            "Destination account is incorrect or belongs to a different bank.";

        [MaxLength(ModelConstants.Transaction.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Transaction.MinStartingPrice,
            ModelConstants.Transaction.MaxStartingPrice, ErrorMessage =
                "The amount cannot be lower than 0.01.")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Destination account")]
        [RegularExpression(@"^[A-Z]{4}\d{8}$", ErrorMessage = DestinationAccountIncorrectError)]
        public string DestinationBankAccountUniqueId { get; set; }

        public IEnumerable<OwnBankAccountListingViewModel> OwnAccounts { get; set; }

        [Required]
        [Display(Name = "Source account")]
        public string AccountId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var uniqueIdHelper = validationContext.GetService<IBankAccountUniqueIdHelper>();
            if (!uniqueIdHelper.IsUniqueIdValid(this.DestinationBankAccountUniqueId))
            {
                yield return new ValidationResult(DestinationAccountIncorrectError,
                    new[] { nameof(this.DestinationBankAccountUniqueId) });
            }
        }
    }
}
