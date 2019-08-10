using CodexBank.Common.Configuration;
using CodexBank.Data;
using CodexBank.Services.Tests.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace CodexBank.Services.Tests.Tests
{
    public abstract class BaseTest
    {
        private const string SampleBankName = "Codex bank";
        private const string SampleUniqueIdentifier = "ABC";
        private const string SampleFirst3CardDigits = "123";
        private const string SampleCodexApiAddress = "https://localhost:5001/";
        private const string SampleCodexApiPublicKey = "sdgijsd09gusd0jsdpfasjiofasd";
        private const string SampleBankCountry = "Bulgaria";
        private const string SampleBankKey = "sdf90234rewfsd0ij9oigsdf";

        protected BaseTest()
        {
            TestSetup.InitializeMapper();
        }

        protected CodexBankDbContext DatabaseInstance
        {
            get
            {
                var options = new DbContextOptionsBuilder<CodexBankDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options;

                return new CodexBankDbContext(options);
            }
        }

        protected Mock<IOptions<BankConfiguration>> MockedBankConfiguration
        {
            get
            {
                var bankConfiguration = new BankConfiguration
                {
                    BankName = SampleBankName,
                    UniqueIdentifier = SampleUniqueIdentifier,
                    First3CardDigits = SampleFirst3CardDigits,
                    CodexApiAddress = SampleCodexApiAddress,
                    CodexApiPublicKey = SampleCodexApiPublicKey,
                    Country = SampleBankCountry,
                    Key = SampleBankKey
                };

                var options = new Mock<IOptions<BankConfiguration>>();
                options.Setup(x => x.Value).Returns(bankConfiguration);

                return options;
            }
        }
    }
}
