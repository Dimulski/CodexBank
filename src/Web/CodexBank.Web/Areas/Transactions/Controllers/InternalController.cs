using AutoMapper;
using CodexBank.Common;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using CodexBank.Services.Models.Transaction;
using CodexBank.Web.Areas.Transactions.Models.Internal;
using CodexBank.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Web.Areas.Transactions.Controllers
{
    public class InternalController : BaseTransactionController
    {
        private readonly IBankAccountService bankAccountService;
        private readonly ITransactionService transactionService;
        private readonly IUserService userService;

        public InternalController(
            ITransactionService transactionService,
            IBankAccountService bankAccountService,
            IUserService userService)
            : base(bankAccountService)
        {
            this.transactionService = transactionService;
            this.userService = userService;
            this.bankAccountService = bankAccountService;
        }

        public async Task<IActionResult> Create()
        {
            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);
            var userAccounts = await this.GetAllAccountsAsync(userId);

            if (!userAccounts.Any())
            {
                this.ShowErrorMessage(NotificationMessages.NoAccountsError);

                return this.RedirectToHome();
            }

            var model = new InternalTransactionCreateBindingModel
            {
                OwnAccounts = userAccounts
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InternalTransactionCreateBindingModel model)
        {
            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);

            if (!this.ModelState.IsValid)
            {
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var account = await this.bankAccountService.GetByIdAsync<BankAccountDetailsServiceModel>(model.AccountId);
            if (account == null || account.UserUserName != this.User.Identity.Name)
            {
                return this.Forbid();
            }

            if (string.Equals(account.UniqueId, model.DestinationBankAccountUniqueId,
                StringComparison.InvariantCulture))
            {
                this.ShowErrorMessage(NotificationMessages.SameAccountsError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            if (account.Balance < model.Amount)
            {
                this.ShowErrorMessage(NotificationMessages.InsufficientFunds);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var destinationAccount =
                await this.bankAccountService.GetByUniqueIdAsync<BankAccountConciseServiceModel>(
                    model.DestinationBankAccountUniqueId);
            if (destinationAccount == null)
            {
                this.ShowErrorMessage(NotificationMessages.DestinationBankAccountDoesNotExist);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var referenceNumber = ReferenceNumberGenerator.GenerateReferenceNumber();
            var sourceServiceModel = Mapper.Map<TransactionCreateServiceModel>(model);
            sourceServiceModel.Source = account.UniqueId;
            sourceServiceModel.Amount *= -1;
            sourceServiceModel.SenderName = account.UserFullName;
            sourceServiceModel.RecipientName = destinationAccount.UserFullName;
            sourceServiceModel.ReferenceNumber = referenceNumber;

            if (!await this.transactionService.CreateTransactionAsync(sourceServiceModel))
            {
                this.ShowErrorMessage(NotificationMessages.TryAgainLaterError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var destinationServiceModel = Mapper.Map<TransactionCreateServiceModel>(model);
            destinationServiceModel.Source = account.UniqueId;
            destinationServiceModel.AccountId = destinationAccount.Id;
            destinationServiceModel.SenderName = account.UserFullName;
            destinationServiceModel.RecipientName = destinationAccount.UserFullName;
            destinationServiceModel.ReferenceNumber = referenceNumber;

            if (!await this.transactionService.CreateTransactionAsync(destinationServiceModel))
            {
                this.ShowErrorMessage(NotificationMessages.TryAgainLaterError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            this.ShowSuccessMessage(NotificationMessages.SuccessfulTransaction);

            return this.RedirectToHome();
        }
    }
}