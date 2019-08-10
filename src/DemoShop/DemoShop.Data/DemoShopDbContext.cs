using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoShop.Data
{
    public class DemoShopDbContext : IdentityDbContext<DemoShopUser>
    {
        public DemoShopDbContext(DbContextOptions<DemoShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
