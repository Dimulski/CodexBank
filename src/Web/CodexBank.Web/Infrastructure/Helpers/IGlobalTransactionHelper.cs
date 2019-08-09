using CodexBank.Web.Infrastructure.Helpers.Models;
using System.Threading.Tasks;

namespace CodexBank.Web.Infrastructure.Helpers
{
    public interface IGlobalTransactionHelper
    {
        Task<GlobalTransactionResult> TransactAsync(GlobalTransactionDto model);
    }
}
