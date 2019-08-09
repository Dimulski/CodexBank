using CodexApi.Services.Models.Bank;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodexApi.Services.Contracts
{
    public interface IBankService
    {
        Task<T> GetBankAsync<T>(string bankName, string swiftCode, string bankCountry)
            where T : BankBaseServiceModel;

        Task<IEnumerable<T>> GetAllBanksSupportingPaymentsAsync<T>()
            where T : BankBaseServiceModel;

        Task<T> GetBankByIdAsync<T>(string id)
            where T : BankBaseServiceModel;

        Task<T> GetBankByBankIdentificationCardNumbersAsync<T>(string identificationCardNumbers)
            where T : BankBaseServiceModel;
    }
}
