using CodexBank.Common.AutoMapping.Contracts;
using CodexBank.Services.Models.BankAccount;

namespace CodexBank.Web.Models.BankAccount
{
    public class OwnBankAccountListingViewModel : IMapWith<BankAccountIndexServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string UniqueId { get; set; }
    }
}