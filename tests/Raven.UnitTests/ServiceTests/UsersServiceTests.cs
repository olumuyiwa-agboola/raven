using Bogus;
using System.Net;
using FakeItEasy;
using FluentAssertions;
using Raven.Core.Services;
using Raven.Core.Models.Shared;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Entities;
using Raven.Core.Libraries.Enums;
using Raven.Core.Libraries.Constants;
using Raven.Core.Abstractions.Repositories;

namespace Raven.UnitTests.ServiceTests
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task Creating_an_OTP_user_succeeds_if_the_user_is_saved_to_the_database_successfully()
        {
            // Arrange
            var usersRepository = A.Fake<IUsersRepository>();
            var sut = new UsersService(usersRepository);
            var request = GenerateSample<CreateOtpUserRequest>();
            A.CallTo(() => usersRepository.SaveOtpUser(A<OtpUser>.Ignored)).Returns((true, null));

            // Act
            var (isOtpUserCreationSuccessful, createOtpUserResponse, problemDetails) = await sut.CreateOtpUser(request);

            //Assert
            isOtpUserCreationSuccessful.Should().BeTrue();

            createOtpUserResponse.Should().NotBeNull();
            createOtpUserResponse.Message.Should().Contain("successful");
            createOtpUserResponse.UserId.Should().NotBeNullOrWhiteSpace();
            createOtpUserResponse.Message.Should().NotBeNullOrWhiteSpace();
            createOtpUserResponse.CreatedAt.Should().NotBeNullOrWhiteSpace();

            problemDetails.Should().BeNull();
        }

        [Fact]
        public async Task Creating_an_OTP_user_fails_if_saving_the_user_to_the_database_fails()
        {
            // Arrange
            var usersRepository = A.Fake<IUsersRepository>();
            var sut = new UsersService(usersRepository);
            var request = GenerateSample<CreateOtpUserRequest>();
            var error = Error.NewError(ErrorType.RecordAlreadyExists, ErrorMessages.EmailAlreadyExists);  
            A.CallTo(() => usersRepository.SaveOtpUser(A<OtpUser>.Ignored)).Returns((false, error));

            // Act
            var (isOtpUserCreationSuccessful, createOtpUserResponse, problemDetails) = await sut.CreateOtpUser(request);

            //Assert
            isOtpUserCreationSuccessful.Should().BeFalse();

            createOtpUserResponse.Should().BeNull();

            problemDetails.Should().NotBeNull();
            problemDetails.Status.Should().Be((int)HttpStatusCode.Conflict);
            problemDetails.Title.Should().BeEquivalentTo(ErrorMessages.RecordAlreadyExists);
            problemDetails.Detail.Should().BeEquivalentTo(ErrorMessages.EmailAlreadyExists);
        }

        private static T GenerateSample<T>()
        {
            return typeof(T) switch
            {
                Type t when t == typeof(CreateOtpUserRequest) => (T)(object)new Faker<CreateOtpUserRequest>()
                                        .RuleFor(x => x.LastName, x => x.Person.LastName)
                                        .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                                        .RuleFor(x => x.PhoneNumber, x => x.Phone.PhoneNumber())
                                        .RuleFor(x => x.Email, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName))
                                        .Generate(),

                _ => throw new InvalidOperationException($"No sample generator defined for type {typeof(T).Name}."),
            };
        }
    }
}