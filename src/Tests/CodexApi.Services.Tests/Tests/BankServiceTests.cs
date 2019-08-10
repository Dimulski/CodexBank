﻿using CodexApi.Data;
using CodexApi.Models;
using CodexApi.Services.Contracts;
using CodexApi.Services.Implementations;
using CodexApi.Services.Models.Bank;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodexApi.Services.Tests.Tests
{
    public class BankServiceTests : BaseTest
    {
        public BankServiceTests()
        {
            this.dbContext = this.DatabaseInstance;
            this.bankService = new BankService(this.dbContext);
        }

        private const string SampleBankName = "Codex bank";
        private const string SampleBankCountry = "Bulgaria";
        private const string SampleBankSwiftCode = "ABC";
        private const string SamplePaymentUrl = "https://localhost:56013/pay";
        private const string SampleIdentificationNumbers = "10";

        private readonly CodexApiDbContext dbContext;
        private readonly IBankService bankService;

        [Theory]
        [InlineData(" ")]
        [InlineData("    !")]
        [InlineData("totally invalid id")]
        public async Task GetBankByIdAsync_WithInvalidId_ShouldReturnNull(string id)
        {
            // Arrange
            await this.SeedBanks(10);

            // Act
            var result = await this.bankService.GetBankByIdAsync<BankServiceModel>(id);

            // Assert
            result
                .Should()
                .BeNull();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("    !")]
        [InlineData("totally invalid id")]
        public async Task GetBankByBankIdentificationCardNumbersAsync_WithInvalidIdentificationNumber_ShouldReturnNull(string identificationNumbers)
        {
            // Arrange
            await this.SeedBanks(10);

            // Act
            var result = await this.bankService.GetBankByBankIdentificationCardNumbersAsync<BankServiceModel>(identificationNumbers);

            // Assert
            result
                .Should()
                .BeNull();
        }

        private async Task SeedBanks(int count)
        {
            var banks = new List<Bank>();
            for (int i = 1; i <= count; i++)
            {
                var bank = new Bank
                {
                    Id = i.ToString(),
                    Name = $"{SampleBankName}_{i}",
                    Location = $"{SampleBankCountry}_{i}",
                    SwiftCode = $"{SampleBankSwiftCode}_{i}",
                    PaymentUrl = SamplePaymentUrl,
                    BankIdentificationCardNumbers = $"{SampleIdentificationNumbers}{i}"
                };

                banks.Add(bank);
            }

            await this.dbContext.Banks.AddRangeAsync(banks);
            await this.dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllBanksSupportingPaymentsAsync_ShouldOrderByLocationAndThenByName()
        {
            // Arrange
            await this.SeedBanks(10);

            // Act
            var result = await this.bankService.GetAllBanksSupportingPaymentsAsync<BankListingServiceModel>();

            // Assert
            result
                .Should()
                .BeInAscendingOrder(b => b.Location)
                .And
                .BeInAscendingOrder(b => b.Name);
        }

        [Fact]
        public async Task GetAllBanksSupportingPaymentsAsync_ShouldReturnCorrectModel()
        {
            // Arrange
            await this.SeedBanks(3);

            // Act
            var result = await this.bankService.GetAllBanksSupportingPaymentsAsync<BankListingServiceModel>();

            // Assert
            result
                .Should()
                .AllBeAssignableTo<BankListingServiceModel>();
        }

        [Fact]
        public async Task GetAllBanksSupportingPaymentsAsync_ShouldReturnOnlyBanks_With_NonNullablePaymentUrls()
        {
            // Arrange
            const int count = 10;
            await this.SeedBanks(count);

            // Seed one more bank which doesn't support payments
            await this.dbContext.Banks.AddAsync(new Bank());
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.bankService.GetAllBanksSupportingPaymentsAsync<BankListingServiceModel>();

            // Assert
            result
                .Should()
                .HaveCount(count);
        }

        [Fact]
        public async Task GetBankAsync_WithInvalidBankCountry_ShouldReturnNull()
        {
            // Arrange
            await this.SeedBanks(5);

            // Act
            var result = await this.bankService
                .GetBankAsync<BankServiceModel>(SampleBankName, null, SampleBankSwiftCode);

            // Assert
            result
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task GetBankAsync_WithInvalidBankName_ShouldReturnNull()
        {
            // Arrange
            await this.SeedBanks(10);

            // Act
            var result = await this.bankService
                .GetBankAsync<BankServiceModel>(null, SampleBankCountry, SampleBankSwiftCode);

            // Assert
            result
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task GetBankAsync_WithInvalidBankSwiftCode_ShouldReturnNull()
        {
            // Arrange
            await this.SeedBanks(3);

            // Act
            var result = await this.bankService
                .GetBankAsync<BankServiceModel>(SampleBankName, SampleBankCountry, null);

            // Assert
            result
                .Should()
                .BeNull();
        }

        [Fact]
        public async Task GetBankAsync_WithValidArguments_ShouldReturnCorrectEntity()
        {
            // Arrange
            await this.SeedBanks(3);
            var expectedBank = await this.dbContext.Banks.FirstOrDefaultAsync();

            // Act
            var result = await this.bankService
                .GetBankAsync<BankListingServiceModel>(expectedBank.Name, expectedBank.SwiftCode, expectedBank.Location);

            // Assert
            result
                .Should()
                .Match(b => b.As<BankListingServiceModel>().Id == expectedBank.Id);
        }

        [Fact]
        public async Task GetBankByBankIdentificationCardNumbersAsync_WitValidIdentificationNumber_ShouldReturnCorrectEntity()
        {
            // Arrange
            const string expectedId = "1";
            await this.dbContext.Banks.AddAsync(new Bank { Id = expectedId, BankIdentificationCardNumbers = SampleIdentificationNumbers });
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.bankService.GetBankByBankIdentificationCardNumbersAsync<BankListingServiceModel>(SampleIdentificationNumbers);

            // Assert
            result
                .Should()
                .Match(x => x.As<BankListingServiceModel>().Id == expectedId);
        }

        [Fact]
        public async Task GetBankByIdAsync_WitValidId_ShouldReturnCorrectEntity()
        {
            // Arrange
            await this.SeedBanks(10);
            var bank = await this.dbContext.Banks.FirstOrDefaultAsync();
            // Act
            var result = await this.bankService.GetBankByIdAsync<BankListingServiceModel>(bank.Id);

            // Assert
            result
                .Should()
                .Match(x => x.As<BankListingServiceModel>().Id == bank.Id);
        }

        [Fact]
        public async Task GetBankByIdAsync_WitValidId_ShouldReturnCorrectModel()
        {
            // Arrange
            await this.SeedBanks(10);
            var bank = await this.dbContext.Banks.FirstOrDefaultAsync();
            // Act
            var result = await this.bankService.GetBankByIdAsync<BankListingServiceModel>(bank.Id);

            // Assert
            result
                .Should()
                .BeAssignableTo<BankListingServiceModel>();
        }
    }
}
