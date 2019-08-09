﻿using CodexApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodexApi.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private const int CookieValidityInMinutes = 5;
        private const string PaymentDataCookie = "PaymentData";
        private const string PaymentDataFormKey = "data";

        private readonly IBankService bankService;
        private readonly CodexApiConfiguration configuration;

        public PaymentsController(IBankService bankService, IOptions<CodexApiConfiguration> configuration)
        {
            this.bankService = bankService;
            this.configuration = configuration.Value;
        }

        [HttpPost]
        [Route("/pay")]
        public IActionResult SetCookie(string data)
        {
            string decodedData;

            try
            {
                decodedData = DirectPaymentsHelper.DecodePaymentRequest(data);
            }
            catch
            {
                return this.BadRequest();
            }

            // set payment data cookie
            this.Response.Cookies.Append(PaymentDataCookie, decodedData,
                new CookieOptions
                {
                    SameSite = SameSiteMode.Lax,
                    HttpOnly = true,
                    IsEssential = true,
                    MaxAge = TimeSpan.FromMinutes(CookieValidityInMinutes)
                });

            return this.RedirectToAction("Process");
        }

        [HttpGet]
        [Route("/pay")]
        public async Task<IActionResult> Process()
        {
            bool cookieExists = this.Request.Cookies.TryGetValue(PaymentDataCookie, out string data);

            if (!cookieExists)
            {
                return this.BadRequest();
            }

            try
            {
                var request = DirectPaymentsHelper.ParsePaymentRequest(data);

                if (request == null)
                {
                    return this.BadRequest();
                }

                var paymentInfo = DirectPaymentsHelper.GetPaymentInfo(request);

                var banks = (await this.bankService.GetAllBanksSupportingPaymentsAsync<BankListingServiceModel>())
                    .Select(Mapper.Map<BankListingViewModel>)
                    .ToArray();

                var viewModel = new PaymentSelectBankViewModel
                {
                    Amount = paymentInfo.Amount,
                    Description = paymentInfo.Description,
                    Banks = banks
                };

                return this.View(viewModel);
            }
            catch
            {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("/pay/continue")]
        public async Task<IActionResult> Continue([FromForm] string bankId)
        {
            bool cookieExists = this.Request.Cookies.TryGetValue(PaymentDataCookie, out string data);

            if (!cookieExists)
            {
                return this.BadRequest();
            }

            try
            {
                var request = DirectPaymentsHelper.ParsePaymentRequest(data);

                if (request == null)
                {
                    return this.BadRequest();
                }

                var bank = await this.bankService.GetBankByIdAsync<BankPaymentServiceModel>(bankId);
                if (bank?.PaymentUrl == null)
                {
                    return this.BadRequest();
                }

                // generate PaymentProof containing the bank's public key
                // and merchant's original PaymentInfo signature
                string proofRequest = DirectPaymentsHelper.GeneratePaymentRequestWithProof(request,
                    bank.ApiKey, this.configuration.Key);

                // redirect the user to their bank for payment completion
                var paymentPostRedirectModel = new PaymentPostRedirectModel
                {
                    Url = bank.PaymentUrl,
                    PaymentDataFormKey = PaymentDataFormKey,
                    PaymentData = proofRequest
                };

                return this.View("PaymentPostRedirect", paymentPostRedirectModel);
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}