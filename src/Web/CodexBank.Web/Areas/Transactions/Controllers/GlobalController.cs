using AutoMapper;
using CodexBank.Common;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using CodexBank.Web.Areas.Transactions.Models.Global.Create;
using CodexBank.Web.Infrastructure.Helpers;
using CodexBank.Web.Infrastructure.Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Web.Areas.Transactions.Controllers
{
    public class GlobalController : BaseTransactionController
    {
        private readonly IBankAccountService bankAccountService;
        private readonly IGlobalTransactionHelper globalTransactionHelper;
        private readonly IUserService userService;

        public GlobalController(
            IBankAccountService bankAccountService,
            IUserService userService,
            IGlobalTransactionHelper globalTransactionHelper)
            : base(bankAccountService)
        {
            this.bankAccountService = bankAccountService;
            this.userService = userService;
            this.globalTransactionHelper = globalTransactionHelper;
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

            var model = new GlobalTransactionCreateBindingModel
            {
                OwnAccounts = userAccounts,
                SenderName = await this.userService.GetAccountOwnerFullNameAsync(userId)
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GlobalTransactionCreateBindingModel model)
        {
            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);
            model.SenderName = await this.userService.GetAccountOwnerFullNameAsync(userId);
            if (!this.TryValidateModel(model))
            {
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var account = await this.bankAccountService.GetByIdAsync<BankAccountDetailsServiceModel>(model.AccountId);
            if (account == null || account.UserUserName != this.User.Identity.Name)
            {
                return this.Forbid();
            }

            if (string.Equals(account.UniqueId, model.DestinationBank.Account.UniqueId,
                StringComparison.InvariantCulture))
            {
                this.ShowErrorMessage(NotificationMessages.SameAccountsError);
                model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                return this.View(model);
            }

            var serviceModel = Mapper.Map<GlobalTransactionDto>(model);
            serviceModel.SourceAccountId = model.AccountId;
            serviceModel.RecipientName = model.DestinationBank.Account.UserFullName;

            var result = await this.globalTransactionHelper.TransactAsync(serviceModel);
            if (result != GlobalTransactionResult.Succeeded)
            {
                if (result == GlobalTransactionResult.InsufficientFunds)
                {
                    this.ShowErrorMessage(NotificationMessages.InsufficientFunds);
                    model.OwnAccounts = await this.GetAllAccountsAsync(userId);

                    return this.View(model);
                }

                this.ShowErrorMessage(NotificationMessages.TryAgainLaterError);
                return this.RedirectToHome();
            }

            this.ShowSuccessMessage(NotificationMessages.SuccessfulTransaction);
            return this.RedirectToHome();
        }
    }
}