using CodexBank.Data;
using CodexBank.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodexBank.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        public UserService(CodexBankDbContext context)
            : base(context)
        {
        }

        public async Task<string> GetUserIdByUsernameAsync(string username)
        {
            var user = await this.context
                .Users
                .SingleOrDefaultAsync(u => u.UserName == username);

            return user?.Id;
        }

        public async Task<string> GetAccountOwnerFullNameAsync(string userId)
        {
            var user = await this.context
                .Users
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user?.FirstName + " " + user?.MiddleName + " " + user?.LastName;
        }
    }
}
