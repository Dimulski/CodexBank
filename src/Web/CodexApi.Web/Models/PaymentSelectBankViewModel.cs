using System.Collections.Generic;

namespace CodexApi.Web.Models
{
    public class PaymentSelectBankViewModel
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public IEnumerable<BankListingViewModel> Banks { get; set; }
    }
}
