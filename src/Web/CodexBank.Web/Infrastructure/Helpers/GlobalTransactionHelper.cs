using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodexBank.Common.Configuration;
using CodexBank.Common.Utils;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using CodexBank.Services.Models.Transaction;
using CodexBank.Web.Infrastructure.Helpers.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CodexBank.Web.Infrastructure.Helpers
{
    public class GlobalTransactionHelper : IGlobalTransactionHelper
    {
        private const string CodexApiTransactionSubmitUrlFormat = "{0}api/ReceiveTransactions";

        private readonly IBankAccountService bankAccountService;
        private readonly BankConfiguration bankConfiguration;
        private readonly ITransactionService TransactionService;

        public GlobalTransactionHelper(
            IBankAccountService bankAccountService,
            ITransactionService transactionService,
            IOptions<BankConfiguration> bankConfigurationOptions)
        {
            this.bankAccountService = bankAccountService;
            this.TransactionService = transactionService;
            this.bankConfiguration = bankConfigurationOptions.Value;
        }

        public async Task<GlobalTransactionResult> TransactAsync(GlobalTransactionDto model)
        {
            if (!ValidationUtil.IsObjectValid(model))
            {
                return GlobalTransactionResult.GeneralFailure;
            }

            var account = await this.bankAccountService
                .GetByIdAsync<BankAccountConciseServiceModel>(model.SourceAccountId);

            // check if account exists and recipient name is accurate
            if (account == null)
            {
                return GlobalTransactionResult.GeneralFailure;
            }

            // verify there is enough money in the account
            if (account.Balance < model.Amount)
            {
                return GlobalTransactionResult.InsufficientFunds;
            }

            // contact the CodexApi to execute the transfer
            var submitDto = Mapper.Map<CodexApiSubmitTransactionDto>(model);
            submitDto.SenderName = account.UserFullName;
            submitDto.SenderAccountUniqueId = account.UniqueId;

            bool remoteSuccess = await this.ContactCodexApiAsync(submitDto);
            if (!remoteSuccess)
            {
                return GlobalTransactionResult.GeneralFailure;
            }

            // remove money from source account
            var serviceModel = new TransactionCreateServiceModel
            {
                Amount = -model.Amount,
                Source = account.UniqueId,
                Description = model.Description,
                AccountId = account.Id,
                DestinationBankAccountUniqueId = model.DestinationBankAccountUniqueId,
                SenderName = account.UserFullName,
                RecipientName = model.RecipientName,
                ReferenceNumber = submitDto.ReferenceNumber
            };

            bool success = await this.TransactionService.CreateTransactionAsync(serviceModel);
            return !success ? GlobalTransactionResult.GeneralFailure : GlobalTransactionResult.Succeeded;
        }

        private async Task<bool> ContactCodexApiAsync(CodexApiSubmitTransactionDto model)
        {
            var encryptedData = this.SignAndEncryptData(model);

            var client = new HttpClient();
            var response = await client.PostAsJsonAsync(
                string.Format(CodexApiTransactionSubmitUrlFormat, this.bankConfiguration.CodexApiAddress),
                encryptedData);

            return response.IsSuccessStatusCode;
        }

        private string SignAndEncryptData(CodexApiSubmitTransactionDto model)
        {
            using (var rsa = RSA.Create())
            {
                RsaExtensions.FromXmlString(rsa, this.bankConfiguration.Key);
                var aesParams = CryptographyExtensions.GenerateKey();
                var key = Convert.FromBase64String(aesParams[0]);
                var iv = Convert.FromBase64String(aesParams[1]);

                var serializedModel = JsonConvert.SerializeObject(model);
                var dataObject = new
                {
                    Model = serializedModel,
                    Timestamp = DateTime.UtcNow
                };

                var data = JsonConvert.SerializeObject(dataObject);

                var signature = Convert.ToBase64String(rsa
                    .SignData(Encoding.UTF8.GetBytes(data), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));

                string encryptedKey;
                string encryptedIv;
                using (var encryptionRsa = RSA.Create())
                {
                    RsaExtensions.FromXmlString(encryptionRsa, this.bankConfiguration.CodexApiPublicKey);
                    encryptedKey = Convert.ToBase64String(encryptionRsa.Encrypt(key, RSAEncryptionPadding.Pkcs1));
                    encryptedIv = Convert.ToBase64String(encryptionRsa.Encrypt(iv, RSAEncryptionPadding.Pkcs1));
                }

                var encryptedData = Convert.ToBase64String(CryptographyExtensions.Encrypt(data, key, iv));

                var json = new
                {
                    BankName = this.bankConfiguration.BankName,
                    BankSwiftCode = this.bankConfiguration.UniqueIdentifier,
                    BankCountry = this.bankConfiguration.Country,
                    EncryptedKey = encryptedKey,
                    EncryptedIv = encryptedIv,
                    Data = encryptedData,
                    Signature = signature
                };

                var jsonRequest = JsonConvert.SerializeObject(json);
                var encryptedRequest = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonRequest));

                return encryptedRequest;
            }
        }
    }
}
