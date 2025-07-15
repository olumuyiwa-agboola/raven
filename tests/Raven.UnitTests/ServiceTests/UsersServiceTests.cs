using Bogus;
using FakeItEasy;
using System.Net;
using FluentAssertions;
using Raven.Core.Services;
using Raven.Core.Models.Shared;
using Raven.Core.Libraries.Enums;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Requests;
using Raven.Core.Libraries.Constants;
using Raven.Core.Abstractions.Repositories;

namespace Raven.UnitTests.ServiceTests
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task Creating_a_user_succeeds_if_saving_the_user_to_the_database_succeeds()
        {
            // Arrange
            var usersRepository = A.Fake<IUsersRepository>();
            A.CallTo(() => usersRepository.SaveUser(A<User>.Ignored)).Returns((true, null));

            var sut = new UsersService(usersRepository);
            var request = GenerateSample<CreateUserRequest>();

            // Act
            var (isUserCreationSuccessful, createUserResponse, problemDetails) = await sut.CreateUser(request);

            //Assert
            isUserCreationSuccessful.Should().BeTrue();

            createUserResponse.Should().NotBeNull();
            createUserResponse.UserId.Should().NotBeNullOrWhiteSpace();

            problemDetails.Should().BeNull();
        }

        [Fact]
        public async Task Creating_a_user_fails_if_saving_the_user_to_the_database_fails()
        {
            // Arrange
            var usersRepository = A.Fake<IUsersRepository>();
            var sut = new UsersService(usersRepository);
            var request = GenerateSample<CreateUserRequest>();
            var error = Error.NewError(ErrorType.RecordAlreadyExists, ErrorMessages.EmailAlreadyExists);  
            A.CallTo(() => usersRepository.SaveUser(A<User>.Ignored)).Returns((false, error));

            // Act
            var (isUserCreationSuccessful, createUserResponse, problemDetails) = await sut.CreateUser(request);

            //Assert
            isUserCreationSuccessful.Should().BeFalse();

            createUserResponse.Should().BeNull();

            problemDetails.Should().NotBeNull();
            problemDetails.Status.Should().Be((int)HttpStatusCode.Conflict);
            problemDetails.Title.Should().BeEquivalentTo(ErrorMessages.RecordAlreadyExists);
            problemDetails.Detail.Should().BeEquivalentTo(ErrorMessages.EmailAlreadyExists);
        }

        [Fact]
        public async Task Deleting_a_user_fails_if_the_user_ID_does_not_exist()
        {
            // Arrange
            var usersRepository = A.Fake<IUsersRepository>();
            var sut = new UsersService(usersRepository);
            var nonExistentUserId = Ulid.NewUlid().ToString();
            var error = Error.NewError(ErrorType.UserNotFound, "The user was not found in the database.");
            A.CallTo(() => usersRepository.DeleteUser(nonExistentUserId)).Returns((false, error));

            // Act
            var (isUserDeleteSuccessful, problemDetails) = await sut.DeleteUser(nonExistentUserId);

            // Assert
            isUserDeleteSuccessful.Should().BeFalse();

            problemDetails.Should().NotBeNull();
            problemDetails.Detail?.Should().Be(error.Message);
        }

        private static T GenerateSample<T>()
        {
            return typeof(T) switch
            {
                Type t when t == typeof(CreateUserRequest) => (T)(object)new Faker<CreateUserRequest>()
                                        .RuleFor(x => x.LastName, x => x.Person.LastName)
                                        .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                                        .RuleFor(x => x.PhoneNumber, x => x.Phone.PhoneNumber())
                                        .RuleFor(x => x.EmailAddress, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName))
                                        .Generate(),

                _ => throw new InvalidOperationException($"No sample generator defined for type {typeof(T).Name}."),
            };
        }
    }
}