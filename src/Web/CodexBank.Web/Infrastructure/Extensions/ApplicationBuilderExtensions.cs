using CodexBank.Common;
using CodexBank.Data;
using CodexBank.Data.Seeding;
using CodexBank.Models;
using CodexBank.Web.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodexBank.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            InitializeDatabaseAsync(app).GetAwaiter().GetResult();

            return app;
        }

        private static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<CodexBankDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<BankUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                await dbContext.Database.MigrateAsync();
                await SeedUser(userManager, dbContext);

                if (!await roleManager.RoleExistsAsync(GlobalConstants.AdministratorRoleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));
                }
            }
        }

        private static async Task SeedUser(UserManager<BankUser> userManager, CodexBankDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var user = new BankUser
                {
                    Email = "pesho@codexbank.com",
                    FirstName = "Pesho",
                    MiddleName = "Peshev",
                    LastName = "Peshevski",
                    UserName = "pesho@codexbank.com",
                    EmailConfirmed = true,
                    BankAccounts = new List<BankAccount>
                    {
                        new BankAccount
                        {
                            Balance = 10000,
                            CreatedOn = DateTime.UtcNow,
                            Name = "Main account",
                            UniqueId = "ABCJ98131785"
                        }
                    }
                };

                await userManager.CreateAsync(user, "Pass123$");
            }
        }

        public static IApplicationBuilder AddDefaultSecurityHeaders(
            this IApplicationBuilder app,
            SecurityHeadersBuilder builder)
            => app.UseMiddleware<SecurityHeadersMiddleware>(builder.Policy());

        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider
                    .GetRequiredService<CodexBankDbContext>();

                context.Database.EnsureCreated();

                Assembly.GetAssembly(typeof(CodexBankDbContext))
                    .GetTypes()
                    .Where(type => typeof(ISeeder).IsAssignableFrom(type))
                    .Where(type => type.IsClass)
                    .Select(type => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(type))
                    .ToList()
                    .ForEach(seeder => seeder.Seed().GetAwaiter().GetResult());
            }
        }
    }
}
