using CodexBank.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CodexBank.Data.Seeding
{
    public class BankAdminUserSeeder : ISeeder
    {
        private readonly UserManager<BankUser> userManager;

        public BankAdminUserSeeder(UserManager<BankUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            var user = new BankUser
            {
                UserName = "Codex",
                Email = "codex@codexbank.com",
                FirstName = "Codex",
                MiddleName = "The",
                LastName = "God-Emperor",
            };

            var result = await this.userManager.CreateAsync(user, "codex");
        }
    }
}
