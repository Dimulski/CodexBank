using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodexBank.Data;
using CodexBank.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CodexBank.Web.Controllers
{
    public class BankAccountsController : BaseController
    {
        private readonly CodexBankDbContext context;
        private readonly IBankAccountService bankAccountService;

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