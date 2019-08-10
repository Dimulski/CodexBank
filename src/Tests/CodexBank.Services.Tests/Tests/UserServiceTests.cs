using CodexBank.Data;
using CodexBank.Models;
using CodexBank.Services.Contracts;
using CodexBank.Services.Implementations;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CodexBank.Services.Tests.Tests
{
    public class UserServiceTests : BaseTest
    {
        public UserServiceTests()
        {
            this.dbContext = this.DatabaseInstance;
            this.userService = new UserService(this.dbContext);
        }

        private const string SampleUserId = "dsgsdg-dsg364tr-egdfb-jfd";
        private const string SampleUsername = "pesho";
        private const string SampleUserFullName = "Pesho Peshev";

        private readonly CodexBankDbContext dbContext;
        private readonly IUserService userService;

        [Theory]
        [InlineData(" ")]
        [InlineData("asd  1 ")]
        [InlineData("     10   ")]
        [InlineData("5215@%*)%@")]
        public async Task GetUserIdByUsernameAsync_WithInvalidUsername_ShouldReturnNull(string username)
        {
            // Arrange
            await this.SeedUserAsync();
            // Act
            var result = await this.userService.GetUserIdByUsernameAsync(username);

            // Assert
            result
                .Should()
                .BeNull();
        }

        [Theory]
        [InlineData("  !")]
        [InlineData("asd  1 ")]
        [InlineData("1246  10   ")]
        [InlineData("sdg-sdgfgscx-124r-dhf-")]
        public async Task GetAccountOwnerFullnameAsync_WithInvalidUsername_ShouldReturnNull(string id)
        {
            // Arrange
            await this.SeedUserAsync();
            // Act
            var result = await this.userService.GetAccountOwnerFullNameAsync(id);

            // Assert
            result
                .Should()
                .BeNull();
        }

        private async Task SeedUserAsync()
        {
            await this.dbContext.Users.AddAsync(new BankUser { Id = SampleUserId, UserName = SampleUsername, FullName = SampleUserFullName });
            await this.dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAccountOwnerFullnameAsync_WithValidUsername_ShouldReturnCorrectName()
        {
            // Arrange
            await this.SeedUserAsync();
            // Act
            var result = await this.userService.GetAccountOwnerFullNameAsync(SampleUserId);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .Be(SampleUserFullName);
        }

        [Fact]
        public async Task GetUserIdByUsernameAsync_WithValidUsername_ShouldReturnCorrectId()
        {
            // Arrange
            await this.SeedUserAsync();
            // Act
            var result = await this.userService.GetUserIdByUsernameAsync(SampleUsername);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .Be(SampleUserId);
        }
    }
}
