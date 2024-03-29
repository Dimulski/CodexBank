﻿using CodexBank.Common;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Web.Infrastructure.Helpers.Models
{
    public class GlobalTransactionDto
    {
        [Required]
        public string SourceAccountId { get; set; }

        [Required]
        [Range(typeof(decimal), ModelConstants.Transaction.MinStartingPrice, ModelConstants.Transaction.MaxStartingPrice)]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string RecipientName { get; set; }

        [MaxLength(ModelConstants.Transaction.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.SwiftCodeMaxLength)]
        public string DestinationBankSwiftCode { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.NameMaxLength)]
        public string DestinationBankName { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.CountryMaxLength)]
        public string DestinationBankCountry { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        public string DestinationBankAccountUniqueId { get; set; }
    }
}
