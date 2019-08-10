using CodexApi.Data;
using CodexApi.Services.Tests.Setup;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodexApi.Services.Tests.Tests
{
    public abstract class BaseTest
    {
        protected BaseTest()
        {
            TestSetup.InitializeMapper();
        }

        protected CodexApiDbContext DatabaseInstance
        {
            get
            {
                var options = new DbContextOptionsBuilder<CodexApiDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options;

                return new CodexApiDbContext(options);
            }
        }
    }
}
