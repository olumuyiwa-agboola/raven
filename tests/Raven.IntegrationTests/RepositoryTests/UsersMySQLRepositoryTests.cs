using FluentAssertions;
using Raven.Core.Models.Entities;
using Raven.IntegrationTests.Fixtures;
using Raven.Core.Abstractions.Factories;
using Raven.IntegrationTests.Data.TestData;
using Raven.IntegrationTests.Data.Migrations;
using Raven.Infrastructure.Repositories.MySQL;
using Microsoft.Extensions.DependencyInjection;

namespace Raven.IntegrationTests.RepositoryTests
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
        public async Task Saving_a_user_to_the_database_succeeds_if_the_user_ID_email_address_and_phone_number_do_not_already_exist()
        {
            // Arrange
            User newUser = OtpUsers.Generate(1).First();

            // Act
            var (isSavedSuccessfully, error) = await _usersMySQLRepository.SaveOtpUser(newUser);

            // Assert
            isSavedSuccessfully.Should().BeTrue();
            error.Should().BeNull();
        }

        [Fact]
        public async Task Saving_a_user_to_the_database_fails_if_the_user_ID__or_email_address_or_phone_number_already_exists()
        {
            // Arrange
            User existingUser = OtpUsersTable.SeedData[0];

            // Act
            var (isSavedSuccessfully, error) = await _usersMySQLRepository.SaveOtpUser(existingUser);

            // Assert
            isSavedSuccessfully.Should().BeFalse();
            error.Should().NotBeNull();
        }

        [Fact]
        public async Task Deleting_a_user_from_the_database_succeeds_if_the_user_ID_exists()
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
        public async Task Deleting_a_user_from_the_database_fails_if_the_user_ID_does_not_exist()
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
        public async Task Getting_a_user_from_the_database_succeeds_if_the_user_ID_exists()
        {
            // Arrange
            User existingUser = OtpUsersTable.SeedData[3];

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
        public async Task Getting_a_user_from_the_database_fails_if_the_user_ID_does_not_exist()
        {
            // Arrange
            User nonExistentUser = OtpUsers.Generate(1).First();

            // Act
            var (isRetrievedSuccessfully, otpUser, error) = await _usersMySQLRepository.GetOtpUser(nonExistentUser.UserId);

            // Assert
            isRetrievedSuccessfully.Should().BeFalse();
            otpUser.Should().BeNull();
            error.Should().NotBeNull();
        }

        [Fact]
        public async Task Updating_a_user_succeeds_if_the_user_ID_exists_and_at_least_one_attribute_to_update_is_provided()
        {
            // Arrange
            var existingUser = OtpUsersTable.SeedData[4];
            string newEmailAddress = "sampletest@gmail.com";

            // Act
            var (isUpdatedSuccessfully, userUpdateError) = await _usersMySQLRepository.UpdateOtpUser(
                userId: existingUser.UserId,
                emailAddress: newEmailAddress);

            // Assert
            isUpdatedSuccessfully.Should().BeTrue();
            userUpdateError.Should().BeNull();
        }
    }
}