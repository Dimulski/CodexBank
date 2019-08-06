using CodexBank.Common.AutoMapping.Contracts;
using CodexBank.Services.Models.Transaction;
using System;

namespace CodexBank.Web.Areas.Transactions.Models
{
    public class TransactionListingDto : IMapWith<TransactionListingServiceModel>
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public DateTime MadeOn { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string ReferenceNumber { get; set; }
    }
}
