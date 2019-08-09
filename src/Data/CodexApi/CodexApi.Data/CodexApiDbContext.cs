using CodexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodexApi.Data
{
    public class CodexApiDbContext : DbContext
    {
        public CodexApiDbContext(DbContextOptions<CodexApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
    }
}
