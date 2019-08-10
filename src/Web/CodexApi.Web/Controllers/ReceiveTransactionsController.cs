using AutoMapper;
using CodexApi.Services.Contracts;
using CodexApi.Services.Models.Bank;
using CodexApi.Web.Infrastructure.Filters;
using CodexApi.Web.Infrastructure.Helpers;
using CodexApi.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodexApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DecryptAndVerifyRequest]
    public class ReceiveTransactionsController : ControllerBase
    {
        private const string BankRefusedTheRequestMessage = "{0} refused the transfer. If this error continues to occur please contact our support center.";
        private const string BankNotFound = "{0} was not found.";

        private readonly IBankService bankService;
        private readonly CodexApiConfiguration configuration;

        public ReceiveTransactionsController(IBankService bankService, IOptions<CodexApiConfiguration> configuration)
        {
            this.bankService = bankService;
            this.configuration = configuration.Value;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string data)
        {
            var model = JsonConvert.DeserializeObject<ReceiveTransactionModel>(data);
            var bank = await this.bankService.GetBankAsync<BankServiceModel>(model.DestinationBankName, model.DestinationBankSwiftCode,
                model.DestinationBankCountry);

            if (bank == null)
            {
                return this.NotFound(string.Format(BankNotFound, model.DestinationBankName));
            }

            var sendModel = Mapper.Map<SendTransactionModel>(model);
            var encryptedAndSignedData = TransactionHelper.SignAndEncryptData(sendModel, this.configuration.Key, bank.ApiKey);
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync(bank.ApiAddress, encryptedAndSignedData);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return this.BadRequest(string.Format(BankRefusedTheRequestMessage, model.DestinationBankName));
            }

            return this.Ok();
        }
    }
}
