using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Data.Seeding
{
    public class BankUserRoleSeeder : ISeeder
    {
        private readonly CodexBankDbContext context;

        public BankUserRoleSeeder(CodexBankDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });
            }

            context.SaveChanges();
        }
    }
}
