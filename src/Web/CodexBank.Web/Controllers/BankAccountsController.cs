using System;
using System.Threading.Tasks;
using AutoMapper;
using CodexBank.Common;
using CodexBank.Common.Configuration;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using CodexBank.Web.Models.BankAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodexBank.Web.Controllers
{
    public class BankAccountsController : BaseController
    {
        private const int ItemsPerPage = 10;

        private readonly IBankAccountService bankAccountService;
        private readonly BankConfiguration bankConfiguration;
        private readonly ITransactionService transactionService;
        private readonly IUserService userService;

        public BankAccountsController(
            IBankAccountService bankAccountService,
            IUserService userService,
            ITransactionService transactionService,
            IOptions<BankConfiguration> bankConfigurationOptions)
        {
            this.bankAccountService = bankAccountService;
            this.userService = userService;
            this.transactionService = transactionService;
            this.bankConfiguration = bankConfigurationOptions.Value;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BankAccountCreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var serviceModel = Mapper.Map<BankAccountCreateServiceModel>(model);
            serviceModel.UserId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);

            var accountId = await this.bankAccountService.CreateAsync(serviceModel);
            if (accountId == null)
            {
                this.ShowErrorMessage(NotificationMessages.BankAccountCreateError);

                return this.View(model);
            }

            this.ShowSuccessMessage(NotificationMessages.BankAccountCreated);

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(string id, int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var account = await this.bankAccountService.GetByIdAsync<BankAccountDetailsServiceModel>(id);
            if (account == null ||
                account.UserUserName != this.User.Identity.Name)
            {
                return this.Forbid();
            }

            var allTransfersCount = await this.moneyTransferService.GetCountOfAllMoneyTransfersForAccountAsync(id);
            var transfers = (await this.moneyTransferService
                .GetMoneyTransfersForAccountAsync<MoneyTransferListingServiceModel>(id, pageIndex, ItemsPerPage))
                .Select(Mapper.Map<MoneyTransferListingDto>)
                .ToPaginatedList(allTransfersCount, pageIndex, ItemsPerPage);

            var viewModel = Mapper.Map<BankAccountDetailsViewModel>(account);
            viewModel.MoneyTransfers = transfers;
            viewModel.MoneyTransfersCount = allTransfersCount;
            viewModel.BankName = this.bankConfiguration.BankName;
            viewModel.BankCode = this.bankConfiguration.UniqueIdentifier;
            viewModel.BankCountry = this.bankConfiguration.Country;

            return this.View(viewModel);
        }
    }
}