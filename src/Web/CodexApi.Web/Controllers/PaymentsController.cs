using CodexApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodexApi.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IBankService banksService;
        private readonly CodexApiConfiguration configuration;

        public PaymentsController(IBankService bankService, IOptions<CodexApiConfiguration> configuration)
        {
            this.banksService = bankService;
            this.configuration = configuration.Value;
        }
    }
}