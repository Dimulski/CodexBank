using AutoMapper;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.Transaction;
using CodexBank.Web.Areas.Transactions.Models;
using CodexBank.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Web.Areas.Transactions.Controllers
{
    public class HomeController : BaseTransactionController
    {
        private const int PaymentsCountPerPage = 10;

        private readonly ITransactionService transactionService;
        private readonly IUserService userService;

        public HomeController(
            IBankAccountService bankAccountService,
            ITransactionService transactionService,
            IUserService userService)
            : base(bankAccountService)
        {
            this.transactionService = transactionService;
            this.userService = userService;
        }

        [Route("/{area}/Archives")]
        public async Task<IActionResult> All(int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);
            var allTransactions =
                (await this.transactionService.GetTransactionsAsync<TransactionListingServiceModel>(userId, pageIndex, PaymentsCountPerPage))
                .Select(Mapper.Map<TransactionListingDto>)
                .ToPaginatedList(await this.transactionService.GetCountOfAllTransactionsForUserAsync(userId), pageIndex, PaymentsCountPerPage);

            var transactions = new TransactionListingViewModel
            {
                Transactions = allTransactions
            };

            return this.View(transactions);
        }
    }
}
