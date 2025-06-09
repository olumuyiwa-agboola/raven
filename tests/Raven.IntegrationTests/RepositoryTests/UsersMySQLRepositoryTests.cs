using Bogus;
using FluentAssertions;
using Raven.Core.Models.DTOs;
using Raven.Core.Libraries.Enums;
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
        private readonly UsersMySQLRepository sut;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private static readonly Faker _faker = new Faker(locale: "en_NG");

        public UsersMySQLRepositoryTests(RavenMySQLDbFixture _ravenMySQLDbFixture)
        {
            _dbConnectionFactory = _ravenMySQLDbFixture.Services!.GetRequiredService<IDbConnectionFactory>();
            sut = new UsersMySQLRepository(_dbConnectionFactory);
        }

        [Fact]
        public async Task Saving_a_user_to_the_database_succeeds_if_the_user_ID_email_address_and_phone_number_do_not_already_exist()
        {
            // Arrange
            User newUser = OtpUsers.Generate(1).First();

            // Act
            var (isSavedSuccessfully, error) = await sut.SaveUser(newUser);

            // Assert
            isSavedSuccessfully.Should().BeTrue();
            error.Should().BeNull();
        }

        [Fact]
        public async Task Saving_a_user_to_the_database_fails_if_the_user_ID_or_email_address_or_phone_number_already_exists()
        {
            // Arrange
            User existingUser = OtpUsersTable.SeedData[0];

            // Act
            var (isSavedSuccessfully, error) = await sut.SaveUser(existingUser);

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
            var (isDeletedSuccessfully, error) = await sut.DeleteUser(existingUserId);

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
            var (isDeletedSuccessfully, error) = await sut.DeleteUser(nonExistentUserId);

            // Assert
            isDeletedSuccessfully.Should().BeFalse();

            error.Should().NotBeNull();
            error.Type.Should().Be(ErrorType.NotFound);
        }

        [Fact]
        public async Task Getting_a_user_from_the_database_succeeds_if_the_user_ID_exists()
        {
            // Arrange
            User existingUser = OtpUsersTable.SeedData[3];

            // Act
            var (isRetrievedSuccessfully, otpUser, error) = await sut.GetUser(existingUser.UserId);

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
            var (isRetrievedSuccessfully, otpUser, error) = await sut.GetUser(nonExistentUser.UserId);

            // Assert
            isRetrievedSuccessfully.Should().BeFalse();
            otpUser.Should().BeNull();
            error.Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(UserUpdateTestData))] // Arrange
        public async Task Updating_a_user_succeeds_if_the_user_ID_exists_and_at_least_one_attribute_to_update_is_provided(string userId, UserUpdateDto updates)
        {
            // Act
            var (isUpdatedSuccessfully, userUpdateError) = await sut.UpdateUser(userId, updates);

            // Assert
            isUpdatedSuccessfully.Should().BeTrue();
            userUpdateError.Should().BeNull();
        }

        public static List<object[]> UserUpdateTestData()
        {
            return new List<object[]>
            {
                new object[] { OtpUsersTable.SeedData[4].UserId,  new UserUpdateDto(emailAddress: _faker.Internet.Email()) },
                new object[] { OtpUsersTable.SeedData[4].UserId,  new UserUpdateDto(emailAddress: _faker.Internet.Email(), phoneNumber: _faker.Phone.PhoneNumber()) },
                new object[] { OtpUsersTable.SeedData[4].UserId,  new UserUpdateDto(emailAddress: _faker.Internet.Email(), phoneNumber: _faker.Phone.PhoneNumber(), firstName: _faker.Name.FirstName()), },
                new object[] { OtpUsersTable.SeedData[4].UserId,  new UserUpdateDto(emailAddress: _faker.Internet.Email(), phoneNumber: _faker.Phone.PhoneNumber(), firstName: _faker.Name.FirstName(), lastName: _faker.Name.LastName()), },
            };
        }

        [Fact]
        public async Task Updating_a_user_fails_if_the_user_ID_does_not_exist()
        {
            // Arrange
            string userId = OtpUsers.Generate(1).First().UserId;
            UserUpdateDto updates = new(emailAddress: _faker.Internet.Email());

            // Act
            var (isUpdatedSuccessfully, userUpdateError) = await sut.UpdateUser(userId, updates);

            // Assert
            isUpdatedSuccessfully.Should().BeFalse();
            userUpdateError.Should().NotBeNull();
        }
    }
}