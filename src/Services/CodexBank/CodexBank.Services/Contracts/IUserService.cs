using System.Threading.Tasks;

namespace CodexBank.Services.Contracts
{
    public interface IUserService
    {
        Task<string> GetUserIdByUsernameAsync(string username);
        Task<string> GetAccountOwnerFullNameAsync(string userId);
    }
}
