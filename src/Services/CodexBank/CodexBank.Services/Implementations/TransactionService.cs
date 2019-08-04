using CodexBank.Services.Contracts;
using CodexBank.Services.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodexBank.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IEmailSender emailSender;

        public Task<bool> CreateTransactionAsync<T>(T model) where T : TransactionBaseServiceModel
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountOfAllTransactionsForAccountAsync(string accountId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountOfAllTransactionsForUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetLast10TransactionsForUserAsync<T>(string userId) where T : TransactionBaseServiceModel
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetTransactionAsync<T>(string referenceNumber) where T : TransactionBaseServiceModel
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetTransactionsAsync<T>(string userId, int pageIndex = 1, int count = int.MaxValue) where T : TransactionBaseServiceModel
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetTransactionsForAccountAsync<T>(string accountId, int pageIndex = 1, int count = int.MaxValue) where T : TransactionBaseServiceModel
        {
            throw new NotImplementedException();
        }
    }
}
