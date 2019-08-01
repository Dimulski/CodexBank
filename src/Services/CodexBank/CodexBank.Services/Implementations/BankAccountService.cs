using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodexBank.Data;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;

namespace CodexBank.Services.Implementations
{
    public class BankAccountService : BaseService, IBankAccountService
    {
        private readonly IBankAccountUniqueIdHelper uniqueIdHelper;

        public BankAccountService(CodexBankDbContext context, IBankAccountUniqueIdHelper uniqueIdHelper)
            : base(context)
        {
            this.uniqueIdHelper = uniqueIdHelper;
        }

        public Task<string> CreateAsync(BankAccountCreateServiceModel model)
        {
            if (!this.IsEntityStateValid(model) ||
                !this.context.Users.Any(u => u.Id == model.UserId))
            {
                return null;
            }

            string generatedUniqueId;
            do
            {
                generatedUniqueId = this.uniqueIdHelper.GenerateAccountUniqueId();
            } while (this.context.Accounts.Any(a => a.UniqueId == generatedUniqueId));

            if (model.Name == null)
            {
                model.Name = generatedUniqueId;
            }

            var dbModel = Mapper.Map<BankAccount>(model);
            dbModel.UniqueId = generatedUniqueId;

            await this.Context.Accounts.AddAsync(dbModel);
            await this.Context.SaveChangesAsync();

            return dbModel.Id;
        }

        public Task<bool> ChangeAccountNameAsync(string accountId, string newName)
        {
            throw new System.NotImplementedException();
        }

        

        public Task<IEnumerable<T>> GetAccountsAsync<T>(int pageIndex = 1, int count = int.MaxValue) where T : BankAccountBaseServiceModel
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAccountsByUserIdAsync<T>(string userId) where T : BankAccountBaseServiceModel
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByIdAsync<T>(string id) where T : BankAccountBaseServiceModel
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByUniqueIdAsync<T>(string uniqueId) where T : BankAccountBaseServiceModel
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetCountOfAccountsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
