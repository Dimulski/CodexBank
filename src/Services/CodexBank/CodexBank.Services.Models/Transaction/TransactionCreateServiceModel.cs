﻿using AutoMapper;
using CodexBank.Common;
using CodexBank.Common.AutoMapping.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Services.Models.Transaction
{
    using CodexBank.Models;

    public class TransactionCreateServiceModel : TransactionBaseServiceModel, IHaveCustomMapping
    {
        [MaxLength(ModelConstants.Transaction.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AccountId { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string SenderName { get; set; }

        [Required]
        [MaxLength(ModelConstants.User.FullNameMaxLength)]
        public string RecipientName { get; set; }

        [Required]
        public DateTime MadeOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        public string Source { get; set; }

        [Required]
        [MaxLength(ModelConstants.BankAccount.UniqueIdMaxLength)]
        public string DestinationBankAccountUniqueId { get; set; }

        [Required]
        public string ReferenceNumber { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<TransactionCreateServiceModel, Transaction>()
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.DestinationBankAccountUniqueId));
        }
    }
}
