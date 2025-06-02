using Bogus;
using System.Net;
using FakeItEasy;
using FluentAssertions;
using Raven.Core.Services;
using Raven.Core.Models.Shared;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Entities;
using Raven.Core.Libraries.Enums;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Tests.UnitTests.ServiceTests
{
    public class UsersServiceTests
    {
        private readonly UsersService _usersService;
        private readonly IUsersRepository _usersRepository;
        private readonly CreateOtpUserRequest _sampleCreateOtpUserRequest;
        public UsersServiceTests()
        {
            _usersRepository = A.Fake<IUsersRepository>();
            _usersService = new UsersService(_usersRepository);

            _sampleCreateOtpUserRequest = new Faker<CreateOtpUserRequest>()
             .RuleFor(x => x.LastName, x => x.Person.LastName)
             .RuleFor(x => x.FirstName, x => x.Person.FirstName)
             .RuleFor(x => x.PhoneNumber, x => x.Phone.PhoneNumber())
             .RuleFor(x => x.Email, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName))
             .Generate();
        }

        [Fact]
        public async Task CreateOtpUser_TakesValidCreateOtpUserRequestObject_ReturnsTupleOfTrueAndCreateOtpUserResponseAndNull()
        {
            // Arrange
            A.CallTo(() => _usersRepository.SaveOtpUser(A<OtpUser>.Ignored)).Returns((true, null));

            // Act
            var (isOtpUserCreationSuccessful, createOtpUserResponse, problemDetails) = await _usersService.CreateOtpUser(_sampleCreateOtpUserRequest);

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
        public async Task CreateOtpUser_TakesValidCreateOtpUserRequestObject_ReturnsTupleOfFalseAndNullAndProblemDetails()
        {
            // Arrange
            string errorMessage = "Email address already exists.";
            var error = Error.NewError(ErrorType.RecordAlreadyExists, errorMessage);  
            A.CallTo(() => _usersRepository.SaveOtpUser(A<OtpUser>.Ignored)).Returns((false, error));

            // Act
            var (isOtpUserCreationSuccessful, createOtpUserResponse, problemDetails) = await _usersService.CreateOtpUser(_sampleCreateOtpUserRequest);

            //Assert
            isOtpUserCreationSuccessful.Should().BeFalse();

            createOtpUserResponse.Should().BeNull();

            problemDetails.Should().NotBeNull();
            problemDetails.Detail.Should().BeEquivalentTo(errorMessage);
            problemDetails.Status.Should().Be((int)HttpStatusCode.Conflict);
            problemDetails.Title.Should().BeEquivalentTo("Record already exists.");
        }
    }
}
