using CodexBank.Data.Configurations;
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

        public DbSet<BankAccount> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
