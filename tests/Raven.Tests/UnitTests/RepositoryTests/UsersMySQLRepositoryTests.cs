using FluentAssertions;
using Raven.Tests.TestData;
using Raven.Tests.Migrations;
using Raven.Core.Models.Entities;
using Raven.Core.Abstractions.Factories;
using Raven.Infrastructure.Repositories.MySQL;
using Microsoft.Extensions.DependencyInjection;

namespace Raven.Tests.UnitTests.RepositoryTests
{
    public class UsersMySQLRepositoryTests : IClassFixture<RavenMySQLDbFixture>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly UsersMySQLRepository _usersMySQLRepository;

        public UsersMySQLRepositoryTests(RavenMySQLDbFixture _ravenMySQLDbFixture)
        {
            _dbConnectionFactory = _ravenMySQLDbFixture.Services!.GetRequiredService<IDbConnectionFactory>();
            _usersMySQLRepository = new UsersMySQLRepository(_dbConnectionFactory);
        }

        [Fact]
        public async Task SaveOtpUser_TakesOtpUserObject_ReturnsTupleOfTrueAndNull()
        {
            // Arrange
            OtpUser newUser = OtpUsers.Generate(1).First();

            // Act
            var (isSavedSuccessfully, error) = await _usersMySQLRepository.SaveOtpUser(newUser);

            // Assert
            isSavedSuccessfully.Should().BeTrue();
            error.Should().BeNull();
        }

        [Fact]
        public async Task SaveOtpUser_TakesExistingOtpUserObject_ReturnsTupleOfFalseAndError()
        {
            // Arrange
            OtpUser existingUser = OtpUsersTable.SeedData[0];

            // Act
            var (isSavedSuccessfully, error) = await _usersMySQLRepository.SaveOtpUser(existingUser);

            // Assert
            isSavedSuccessfully.Should().BeFalse();
            error.Should().NotBeNull();
        }
    }
}
