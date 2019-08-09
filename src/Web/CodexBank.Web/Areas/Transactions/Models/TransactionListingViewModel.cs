using CodexBank.Web.Infrastructure.Collections;

namespace CodexBank.Web.Areas.Transactions.Models
{
    public class TransactionListingViewModel
    {
        public PaginatedList<TransactionListingDto> Transactions { get; set; }
    }
}
