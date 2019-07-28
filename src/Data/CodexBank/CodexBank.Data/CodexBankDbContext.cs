using CodexBank.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodexBank.Data
{
    public class CodexBankDbContext : IdentityDbContext<BankUser>
    {
        public CodexBankDbContext(DbContextOptions<CodexBankDbContext> options)
            : base(options)
        {
        }
    }
}
