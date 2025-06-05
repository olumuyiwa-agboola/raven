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
        public async Task SaveOtpUser_TakesExistingOtpUserObject_ReturnsTupleOfFalseAndErrorObject()
        {
            // Arrange
            OtpUser existingUser = OtpUsersTable.SeedData[0];

            // Act
            var (isSavedSuccessfully, error) = await _usersMySQLRepository.SaveOtpUser(existingUser);

            // Assert
            isSavedSuccessfully.Should().BeFalse();
            error.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteOtpUser_TakesExistingOtpUserObject_ReturnsTupleOfTrueAndNull()
        {
            // Arrange
            string existingUserId = OtpUsersTable.SeedData[1].UserId;

            // Act
            var (isDeletedSuccessfully, error) = await _usersMySQLRepository.DeleteOtpUser(existingUserId);

            // Assert
            isDeletedSuccessfully.Should().BeTrue();
            error.Should().BeNull();
        }

        [Fact]
        public async Task DeleteOtpUser_TakesNonExistentOtpUserObject_ReturnsTupleOfFalseAndErrorObject()
        {
            // Arrange
            string nonExistentUserId = OtpUsers.Generate(1).First().UserId;

            // Act
            var (isDeletedSuccessfully, error) = await _usersMySQLRepository.DeleteOtpUser(nonExistentUserId);

            // Assert
            isDeletedSuccessfully.Should().BeFalse();
            error.Should().NotBeNull();
        }

        [Fact]
        public async Task GetOtpUser_TakesExistingOtpUserId_ReturnsTupleOfTrueAndOtpUserObjectAndNull()
        {
            // Arrange
            OtpUser existingUser = OtpUsersTable.SeedData[4];

            // Act
            var (isRetrievedSuccessfully, otpUser, error) = await _usersMySQLRepository.GetOtpUser(existingUser.UserId);

            // Assert
            isRetrievedSuccessfully.Should().BeTrue();
            otpUser.Should().NotBeNull();
            otpUser.UserId.Should().Be(existingUser.UserId);
            otpUser.FirstName.Should().Be(existingUser.FirstName);
            otpUser.LastName.Should().Be(existingUser.LastName);
            otpUser.EmailAddress.Should().Be(existingUser.EmailAddress);
            otpUser.PhoneNumber.Should().Be(existingUser.PhoneNumber);
            error.Should().BeNull();
        }

        [Fact]
        public async Task GetOtpUser_TakesNonExistentOtpUserId_ReturnsTupleOfFalseAndNullAndErrorObject()
        {
            // Arrange
            OtpUser nonExistentUser = OtpUsers.Generate(1).First();

            // Act
            var (isRetrievedSuccessfully, otpUser, error) = await _usersMySQLRepository.GetOtpUser(nonExistentUser.UserId);

            // Assert
            isRetrievedSuccessfully.Should().BeFalse();
            otpUser.Should().BeNull();
            error.Should().NotBeNull();
        }
    }
}
