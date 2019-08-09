using CodexBank.Common;
using CodexBank.Services.Contracts;
using CodexBank.Web.Areas.Transactions.Models.Global.Create;
using CodexBank.Web.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
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
            IGlobalTransactionHelper globalTransferHelper)
            : base(bankAccountService)
        {
            this.bankAccountService = bankAccountService;
            this.userService = userService;
            this.globalTransactionHelper = globalTransferHelper;
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
    }
}
