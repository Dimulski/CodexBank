using System.Threading.Tasks;
using CodexBank.Web.Infrastructure.Helpers.Models;

namespace CodexBank.Web.Infrastructure.Helpers
{
    public class GlobalTransactionHelper : IGlobalTransactionHelper
    {
        public Task<GlobalTransactionResult> TransactAsync(GlobalTransactionDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}
