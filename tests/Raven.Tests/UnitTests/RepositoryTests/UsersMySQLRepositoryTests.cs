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
            OtpUser existingUser = OtpUsersTable.SeedData[3];

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

        [Fact]
        public async Task UpdateOtpUser_TakesAnExistingOtpUserIdAndEmailAddress_ReturnsTupleOfTrueAndNull()
        {
            // Arrange
            OtpUser existingUser = OtpUsersTable.SeedData[4];
            string oldEmailAddress = existingUser.EmailAddress;
            string newEmailAddress = "sample@email.com";

            // Act
            var (isUpdatedSuccessfully, userUpdateError) = await _usersMySQLRepository.UpdateOtpUser(
                userId: existingUser.UserId,
                emailAddress: newEmailAddress);

            var (isRetrievedSuccessfully, updatedOtpUser, userRetrievalError) = await _usersMySQLRepository.GetOtpUser(existingUser.UserId);

            // Assert
            isUpdatedSuccessfully.Should().BeTrue();
            isRetrievedSuccessfully.Should().BeTrue();

            userUpdateError.Should().BeNull();
            userRetrievalError.Should().BeNull();

            updatedOtpUser.Should().NotBeNull();
            updatedOtpUser.UserId.Should().Be(existingUser.UserId);
            updatedOtpUser.FirstName.Should().Be(existingUser.FirstName);
            updatedOtpUser.LastName.Should().Be(existingUser.LastName);
            updatedOtpUser.EmailAddress.Should().Be(newEmailAddress);
            updatedOtpUser.PhoneNumber.Should().Be(existingUser.PhoneNumber);
        }

        [Fact]
        public async Task UpdateOtpUser_TakesAnExistingOtpUserIdAndEmailAddressAndPhoneNumber_ReturnsTupleOfTrueAndNull()
        {
            // Arrange
            OtpUser existingUser = OtpUsersTable.SeedData[5];
            string oldEmailAddress = existingUser.EmailAddress;
            string newEmailAddress = "sample2@email.com";
            string newPhoneNumber = "09091215648";

            // Act
            var (isUpdatedSuccessfully, userUpdateError) = await _usersMySQLRepository.UpdateOtpUser(
                userId: existingUser.UserId,
                emailAddress: newEmailAddress,
                phoneNumber: newPhoneNumber);

            var (isRetrievedSuccessfully, updatedOtpUser, userRetrievalError) = await _usersMySQLRepository.GetOtpUser(existingUser.UserId);

            // Assert
            isUpdatedSuccessfully.Should().BeTrue();
            isRetrievedSuccessfully.Should().BeTrue();

            userUpdateError.Should().BeNull();
            userRetrievalError.Should().BeNull();

            updatedOtpUser.Should().NotBeNull();
            updatedOtpUser.UserId.Should().Be(existingUser.UserId);
            updatedOtpUser.FirstName.Should().Be(existingUser.FirstName);
            updatedOtpUser.LastName.Should().Be(existingUser.LastName);
            updatedOtpUser.EmailAddress.Should().Be(newEmailAddress);
            updatedOtpUser.PhoneNumber.Should().Be(newPhoneNumber);
        }

        [Fact]
        public async Task UpdateOtpUser_TakesAnExistingOtpUserIdAndAnExistingEmailAddress_ReturnsTupleOfFalseAndErrorObject()
        {
            // Arrange
            OtpUser existingUser = OtpUsersTable.SeedData[5];
            string oldEmailAddress = existingUser.EmailAddress;
            string newEmailAddress = "sample2@email.com";

            // Act
            var (isUpdatedSuccessfully, userUpdateError) = await _usersMySQLRepository.UpdateOtpUser(
                userId: existingUser.UserId,
                emailAddress: newEmailAddress);

            var (isRetrievedSuccessfully, updatedOtpUser, userRetrievalError) = await _usersMySQLRepository.GetOtpUser(existingUser.UserId);

            // Assert
            isUpdatedSuccessfully.Should().BeTrue();
            isRetrievedSuccessfully.Should().BeTrue();

            userUpdateError.Should().BeNull();
            userRetrievalError.Should().BeNull();

            updatedOtpUser.Should().NotBeNull();
            updatedOtpUser.UserId.Should().Be(existingUser.UserId);
            updatedOtpUser.FirstName.Should().Be(existingUser.FirstName);
            updatedOtpUser.LastName.Should().Be(existingUser.LastName);
            updatedOtpUser.EmailAddress.Should().Be(newEmailAddress);
        }
    }
}
