using CodexApi.Services.Contracts;
using CodexApi.Services.Models.Bank;
using CodexApi.Web.Infrastructure.Helpers;
using CodexApi.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodexApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardPaymentsController : ControllerBase
    {
        private readonly IBankService bankService;
        private readonly CodexApiConfiguration configuration;

        public CardPaymentsController(IBankService bankService, IOptions<CodexApiConfiguration> configuration)
        {
            this.bankService = bankService;
            this.configuration = configuration.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CardPaymentDto model)
        {
            try
            {
                var first3Digits = model.Number.Substring(0, 3);
                var bank = await this.bankService
                    .GetBankByBankIdentificationCardNumbersAsync<BankPaymentServiceModel>(first3Digits);
                if (bank?.CardPaymentUrl == null)
                {
                    return this.BadRequest();
                }

                var encryptedAndSignedData = TransactionHelper.SignAndEncryptData(model, this.configuration.Key, bank.ApiKey);
                var client = new HttpClient();
                var request = await client.PostAsJsonAsync(bank.CardPaymentUrl, encryptedAndSignedData);

                if (request.StatusCode != HttpStatusCode.OK)
                {
                    return this.BadRequest();
                }

                return this.Ok();
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}
