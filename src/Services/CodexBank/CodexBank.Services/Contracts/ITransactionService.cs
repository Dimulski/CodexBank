using CodexBank.Services.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodexBank.Services.Contracts
{
    public interface ITransactionService
    {
        Task<IEnumerable<T>> GetTransactionsAsync<T>(string userId, int pageIndex = 1, int count = int.MaxValue)
            where T : TransactionBaseServiceModel;

        Task<IEnumerable<T>> GetTransactionsForAccountAsync<T>(string accountId, int pageIndex = 1, int count = int.MaxValue)
            where T : TransactionBaseServiceModel;

        Task<IEnumerable<T>> GetLast10TransactionsForUserAsync<T>(string userId)
            where T : TransactionBaseServiceModel;

        Task<bool> CreateTransactionAsync<T>(T model)
            where T : TransactionBaseServiceModel;

        Task<IEnumerable<T>> GetTransactionAsync<T>(string referenceNumber)
            where T : TransactionBaseServiceModel;

        Task<int> GetCountOfAllTransactionsForAccountAsync(string accountId);

        Task<int> GetCountOfAllTransactionsForUserAsync(string userId);
    }
}
