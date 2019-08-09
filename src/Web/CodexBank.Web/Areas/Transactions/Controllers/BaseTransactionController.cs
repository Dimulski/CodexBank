using AutoMapper;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using CodexBank.Web.Controllers;
using CodexBank.Web.Models.BankAccount;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Web.Areas.Transactions.Controllers
{
    [Area("Transactions")]
    public abstract class BaseTransactionController : BaseController
    {
        private readonly IBankAccountService bankAccountService;

        protected BaseTransactionController(IBankAccountService bankAccountService)
        {
            this.bankAccountService = bankAccountService;
        }

        protected async Task<OwnBankAccountListingViewModel[]> GetAllAccountsAsync(string userId)
        {
            return (await this.bankAccountService
                    .GetAllAccountsByUserIdAsync<BankAccountIndexServiceModel>(userId))
                .Select(Mapper.Map<OwnBankAccountListingViewModel>)
                .ToArray();
        }
    }
}
