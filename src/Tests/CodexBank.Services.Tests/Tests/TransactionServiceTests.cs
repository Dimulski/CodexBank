using CodexBank.Common;
using CodexBank.Common.EmailSender;
using CodexBank.Data;
using CodexBank.Models;
using CodexBank.Services.Contracts;
using CodexBank.Services.Implementations;
using CodexBank.Services.Models.Transaction;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodexBank.Services.Tests.Tests
{
    public class TransactionServiceTests : BaseTest
    {
        public TransactionServiceTests()
        {
            this.dbContext = this.DatabaseInstance;
            this.transactionService = new TransactionService(this.dbContext, Mock.Of<IEmailSender>());
        }

        private const string SampleId = "gsdxcvew-sdfdscx-xcgds";
        private const string SampleDescription = "I'm sending money due to...";
        private const decimal SampleAmount = 10;
        private const string SampleRecipientName = "test";
        private const string SampleSenderName = "pesho";
        private const string SampleDestination = "dgsfcx-arq12wasdasdzxxcv";

        private const string SampleBankAccountName = "Test bank account name";
        private const string SampleBankAccountId = "1";
        private const string SampleBankAccountUniqueId = "UniqueId";
        private const string SampleReferenceNumber = "18832258557125540";

        private const string SampleUserFullname = "user user";
        private const string SampleUserId = "adfsdvxc-123ewsf";

        private readonly CodexBankDbContext dbContext;
        private readonly ITransactionService transactionService;

        [Theory]
        [InlineData(" 031069130864508423")]
        [InlineData("24675875i6452436 ")]
        [InlineData("! 68o4473485669 ")]
        [InlineData("   845768798069  10   ")]
        [InlineData("45848o02835yu56=")]
        public async Task GetTransactionAsync_WithInvalidNumber_ShouldReturnEmptyCollection(string referenceNumber)
        {
            // Arrange
            await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionAsync<TransactionListingServiceModel>(referenceNumber);

            // Assert
            result
                .Should()
                .BeNullOrEmpty();
        }

        [Fact]
        public async Task GetTransactionAsync_WithValidNumber_ShouldReturnCorrectEntities()
        {
            // Arrange
            const string expectedRefNumber = SampleReferenceNumber;
            await this.dbContext.Transactions.AddAsync(new Transaction
            {
                Description = SampleDescription,
                Amount = SampleAmount,
                Account = await this.dbContext.Accounts.FirstOrDefaultAsync(),
                Destination = SampleDestination,
                Source = SampleBankAccountId,
                SenderName = SampleSenderName,
                RecipientName = SampleRecipientName,
                MadeOn = DateTime.UtcNow,
                ReferenceNumber = expectedRefNumber,
            });
            await this.dbContext.SaveChangesAsync();

            // Act
            var result =
                await this.transactionService.GetTransactionAsync<TransactionListingServiceModel>(expectedRefNumber);

            // Assert
            result
                .Should()
                .NotBeNullOrEmpty()
                .And
                .Match(t => t.All(x => x.ReferenceNumber == expectedRefNumber));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" dassdgdf")]
        [InlineData(" ")]
        [InlineData("!  ")]
        [InlineData("     10   ")]
        [InlineData("  sdgsfcx-arq12wasdasdzxxcvc   ")]
        public async Task GetAllTransactionsAsync_WithInvalidUserId_ShouldReturnEmptyCollection(string userId)
        {
            // Arrange
            await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsAsync<TransactionListingServiceModel>(userId);

            // Assert
            result
                .Should()
                .BeNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" dassdgdf")]
        [InlineData(" ")]
        [InlineData("!  ")]
        [InlineData("     10   ")]
        [InlineData("  sdgsfcx-arq12wasdasdzxxcvc   ")]
        public async Task GetAllTransactionsForAccountAsync_WithInvalidUserId_ShouldReturnEmptyCollection(string accountId)
        {
            // Arrange
            await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsForAccountAsync<TransactionListingServiceModel>(accountId);

            // Assert
            result
                .Should()
                .BeNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" dassdgdf")]
        [InlineData(" ")]
        [InlineData("!  ")]
        [InlineData("     10   ")]
        [InlineData("  sdgsfcx-arq12wasdasdzxxcvc   ")]
        public async Task GetLast10TransactionsForUserAsync_WithInvalidUserId_ShouldReturnEmptyCollection(string userId)
        {
            // Arrange
            await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetLast10TransactionsForUserAsync<TransactionListingServiceModel>(userId);

            // Assert
            result
                .Should()
                .BeNullOrEmpty();
        }

        [Fact]
        public async Task GetCountOfAllTransactionsForUserAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            await this.SeedTransactionsAsync();

            // Act
            var result =
                await this.transactionService.GetCountOfAllTransactionsForUserAsync(SampleUserId);

            // Assert
            result
                .Should()
                .Be(await this.dbContext.Transactions.CountAsync(t => t.Account.UserId == SampleUserId));
        }

        [Fact]
        public async Task GetCountOfAllTransactionsForAccountAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            await this.SeedTransactionsAsync();

            // Act
            var result =
                await this.transactionService.GetCountOfAllTransactionsForAccountAsync(SampleBankAccountId);

            // Assert
            result
                .Should()
                .Be(await this.dbContext.Transactions.CountAsync(t => t.AccountId == SampleBankAccountId));
        }

        [Fact]
        public async Task CreateTransactionAsync_WithDifferentId_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Arrange
            await this.SeedBankAccountAsync();
            var model = PrepareCreateModel(accountId: "another id");

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithEmptyModel_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Act
            var result = await this.transactionService.CreateTransactionAsync(new TransactionCreateServiceModel());

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithInvalidDescription_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Arrange
            var invalidDescription = new string('m', ModelConstants.Transaction.DescriptionMaxLength + 1);
            var model = PrepareCreateModel(invalidDescription);

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithInvalidDestinationBankAccountUniqueId_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Arrange
            var invalidDestinationBankAccountUniqueId = new string('m', ModelConstants.BankAccount.UniqueIdMaxLength + 1);
            var model = PrepareCreateModel(destinationBankUniqueId: invalidDestinationBankAccountUniqueId);

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithInvalidRecipientName_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Arrange
            var invalidRecipientName = new string('m', ModelConstants.User.FullNameMaxLength + 1);
            var model = PrepareCreateModel(recipientName: invalidRecipientName);

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithInvalidSenderName_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Arrange
            var invalidSenderName = new string('m', ModelConstants.User.FullNameMaxLength + 1);
            var model = PrepareCreateModel(senderName: invalidSenderName);

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithInvalidSource_ShouldReturnFalse_And_NotAddTransactionInDatabase()
        {
            // Arrange
            var invalidSource = new string('m', ModelConstants.BankAccount.UniqueIdMaxLength + 1);
            var model = PrepareCreateModel(source: invalidSource);

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeFalse();

            this.dbContext
                .Transactions
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task CreateTransactionAsync_WithValidModel_AndNegativeAmount_ShouldReturnTrue_And_InsertTransferInDatabase()
        {
            // Arrange
            var dbCount = this.dbContext.Transactions.Count();
            await this.SeedBankAccountAsync();
            // Setting amount to negative means we're sending money.
            var model = PrepareCreateModel(amount: -10);

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeTrue();

            this.dbContext
                .Transactions
                .Should()
                .HaveCount(dbCount + 1);
        }

        [Fact]
        public async Task CreateTransactionAsync_WithValidModel_ShouldReturnTrue_And_InsertTransferInDatabase()
        {
            // Arrange
            var dbCount = this.dbContext.Transactions.Count();
            await this.SeedBankAccountAsync();
            var model = PrepareCreateModel();

            // Act
            var result = await this.transactionService.CreateTransactionAsync(model);

            // Assert
            result
                .Should()
                .BeTrue();

            this.dbContext
                .Transactions
                .Should()
                .HaveCount(dbCount + 1);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            const int count = 10;
            await this.SeedBankAccountAsync();
            for (int i = 1; i <= count; i++)
            {
                await this.dbContext.Transactions.AddAsync(new Transaction
                {
                    Account = await this.dbContext.Accounts.FirstOrDefaultAsync()
                });
            }

            await this.dbContext.SaveChangesAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsAsync<TransactionListingServiceModel>(SampleUserId);

            // Assert
            result
                .Should()
                .HaveCount(count);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_ShouldReturnOrderedByMadeOnCollection()
        {
            // Arrange
            await this.SeedBankAccountAsync();
            for (int i = 0; i < 10; i++)
            {
                await this.dbContext.Transactions.AddAsync(new Transaction
                {
                    Id = $"{SampleId}_{i}",
                    Account = await this.dbContext.Accounts.FirstOrDefaultAsync(),
                    MadeOn = DateTime.UtcNow.AddMinutes(i)
                });
            }

            await this.dbContext.SaveChangesAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsAsync<TransactionListingServiceModel>(SampleUserId);

            // Assert
            result
                .Should()
                .BeInDescendingOrder(x => x.MadeOn);
        }

        [Fact]
        public async Task GetAllTransactionsAsync_WithValidUserId_ShouldReturnCollectionOfCorrectEntities()
        {
            // Arrange
            var model = await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsAsync<TransactionListingServiceModel>(model.Account.UserId);

            // Assert
            result
                .Should()
                .AllBeAssignableTo<TransactionListingServiceModel>()
                .And
                .Match<IEnumerable<TransactionListingServiceModel>>(x => x.All(c => c.Source == model.Source));
        }

        [Fact]
        public async Task GetAllTransactionsForAccountAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            const int count = 10;
            await this.SeedBankAccountAsync();
            for (int i = 1; i <= count; i++)
            {
                await this.dbContext.Transactions.AddAsync(new Transaction
                {
                    Account = await this.dbContext.Accounts.FirstOrDefaultAsync()
                });
            }

            await this.dbContext.SaveChangesAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsForAccountAsync<TransactionListingServiceModel>(SampleBankAccountId);

            // Assert
            result
                .Should()
                .HaveCount(count);
        }

        [Fact]
        public async Task GetAllTransactionsForAccountAsync_ShouldReturnOrderedByMadeOnCollection()
        {
            // Arrange
            await this.SeedBankAccountAsync();
            for (int i = 0; i < 10; i++)
            {
                await this.dbContext.Transactions.AddAsync(new Transaction
                {
                    Id = $"{SampleId}_{i}",
                    Account = await this.dbContext.Accounts.FirstOrDefaultAsync(),
                    MadeOn = DateTime.UtcNow.AddMinutes(i)
                });
            }

            await this.dbContext.SaveChangesAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsForAccountAsync<TransactionListingServiceModel>(SampleBankAccountId);

            // Assert
            result
                .Should()
                .BeInDescendingOrder(x => x.MadeOn);
        }

        [Fact]
        public async Task GetAllTransactionsForAccountAsync_WithValidUserId_ShouldReturnCollectionOfCorrectEntities()
        {
            // Arrange
            var model = await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetTransactionsForAccountAsync<TransactionListingServiceModel>(model.Account.Id);

            // Assert
            result
                .Should()
                .AllBeAssignableTo<TransactionListingServiceModel>()
                .And
                .Match<IEnumerable<TransactionListingServiceModel>>(x => x.All(c => c.Source == model.Source));
        }

        [Fact]
        public async Task GetLast10TransactionsForUserAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            const int count = 22;
            const int expectedCount = 10;
            await this.SeedBankAccountAsync();
            for (int i = 1; i <= count; i++)
            {
                await this.dbContext.Transactions.AddAsync(new Transaction
                {
                    Account = await this.dbContext.Accounts.FirstOrDefaultAsync()
                });
            }

            await this.dbContext.SaveChangesAsync();
            // Act
            var result =
                await this.transactionService.GetLast10TransactionsForUserAsync<TransactionListingServiceModel>(SampleUserId);

            // Assert
            result
                .Should()
                .HaveCount(expectedCount);
        }

        [Fact]
        public async Task GetLast10TransactionsForUserAsync_ShouldReturnOrderedByMadeOnCollection()
        {
            // Arrange
            await this.SeedBankAccountAsync();
            for (int i = 0; i < 22; i++)
            {
                await this.dbContext.Transactions.AddAsync(new Transaction
                {
                    Id = $"{SampleId}_{i}",
                    Account = await this.dbContext.Accounts.FirstOrDefaultAsync(),
                    MadeOn = DateTime.UtcNow.AddMinutes(i)
                });
            }

            await this.dbContext.SaveChangesAsync();
            // Act
            var result =
                await this.transactionService.GetLast10TransactionsForUserAsync<TransactionListingServiceModel>(SampleUserId);

            // Assert
            result
                .Should()
                .BeInDescendingOrder(x => x.MadeOn);
        }

        [Fact]
        public async Task GetLast10TransactionsForUserAsync_WithValidUserId_ShouldReturnCollectionOfCorrectEntities()
        {
            // Arrange
            var model = await this.SeedTransactionsAsync();
            // Act
            var result =
                await this.transactionService.GetLast10TransactionsForUserAsync<TransactionListingServiceModel>(model.Account.UserId);

            // Assert
            result
                .Should()
                .AllBeAssignableTo<TransactionListingServiceModel>()
                .And
                .Match<IEnumerable<TransactionListingServiceModel>>(x => x.All(c => c.Source == model.Source));
        }

        private static TransactionCreateServiceModel PrepareCreateModel(
            string description = SampleDescription,
            decimal amount = SampleAmount,
            string accountId = SampleBankAccountId,
            string destinationBankUniqueId = SampleDestination,
            string source = SampleBankAccountId,
            string senderName = SampleSenderName,
            string recipientName = SampleRecipientName,
            string referenceNumber = SampleReferenceNumber)
        {
            var model = new TransactionCreateServiceModel
            {
                Description = description,
                Amount = amount,
                AccountId = accountId,
                DestinationBankAccountUniqueId = destinationBankUniqueId,
                Source = source,
                SenderName = senderName,
                RecipientName = recipientName,
                ReferenceNumber = SampleReferenceNumber,
            };

            return model;
        }

        private async Task<Transaction> SeedTransactionsAsync()
        {
            await this.SeedBankAccountAsync();
            var model = new Transaction
            {
                Id = SampleId,
                Description = SampleDescription,
                Amount = SampleAmount,
                Account = await this.dbContext.Accounts.FirstOrDefaultAsync(),
                Destination = SampleDestination,
                Source = SampleBankAccountId,
                SenderName = SampleSenderName,
                RecipientName = SampleRecipientName,
                MadeOn = DateTime.UtcNow
            };

            await this.dbContext.Transactions.AddAsync(model);
            await this.dbContext.SaveChangesAsync();

            return model;
        }

        private async Task<BankAccount> SeedBankAccountAsync()
        {
            await this.SeedUserAsync();
            var model = new BankAccount
            {
                Id = SampleBankAccountId,
                Name = SampleBankAccountName,
                UniqueId = SampleBankAccountUniqueId,
                User = await this.dbContext.Users.FirstOrDefaultAsync()

            };
            await this.dbContext.Accounts.AddAsync(model);
            await this.dbContext.SaveChangesAsync();

            return model;
        }

        private async Task SeedUserAsync()
        {
            await this.dbContext.Users.AddAsync(new BankUser { Id = SampleUserId, FullName = SampleUserFullname });
            await this.dbContext.SaveChangesAsync();
        }
    }
}
