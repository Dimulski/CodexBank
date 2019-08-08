using CodexBank.Web.Areas.Transactions.Models;
using CodexBank.Web.Models.BankAccount;
using System.Collections.Generic;

namespace CodexBank.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<BankAccountIndexViewModel> UserBankAccounts { get; set; }

        public IEnumerable<TransactionListingDto> Transactions { get; set; }
    }
}
