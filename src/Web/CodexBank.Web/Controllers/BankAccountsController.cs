using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodexBank.Common.Configuration;
using CodexBank.Data;
using CodexBank.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CodexBank.Web.Controllers
{
    public class BankAccountsController : BaseController
    {
        private readonly IBankAccountService bankAccountService;
        private readonly BankConfiguration bankConfiguration;
        private readonly ITransactionService transactionService;
        private readonly IUserService userService;

        public BankAccountsController(CodexBankDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}