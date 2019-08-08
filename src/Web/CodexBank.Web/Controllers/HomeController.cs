using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodexBank.Web.Models;
using Microsoft.AspNetCore.Authorization;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using AutoMapper;
using CodexBank.Web.Models.BankAccount;
using CodexBank.Services.Models.Transaction;
using CodexBank.Web.Areas.Transactions.Models;

namespace CodexBank.Web.Controllers
{

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IBankAccountService bankAccountService;
        private readonly ITransactionService transactionService;
        private readonly IUserService userService;

        public HomeController(
            IBankAccountService bankAccountService,
            IUserService userService,
            ITransactionService transactionService)
        {
            this.bankAccountService = bankAccountService;
            this.userService = userService;
            this.transactionService = transactionService;
        }


        public async Task<IActionResult> Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View("IndexGuest");
            }

            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);
            var bankAccounts =
                (await this.bankAccountService.GetAllAccountsByUserIdAsync<BankAccountIndexServiceModel>(userId))
                .Select(Mapper.Map<BankAccountIndexViewModel>)
                .ToArray();
            var transactions = (await this.transactionService
                    .GetLast10TransactionsForUserAsync<TransactionListingServiceModel>(userId))
                .Select(Mapper.Map<TransactionListingDto>)
                .ToArray();

            var viewModel = new HomeViewModel
            {
                UserBankAccounts = bankAccounts,
                Transactions = transactions
            };

            return this.View(viewModel);
        }
    }
}