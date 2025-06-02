using FakeItEasy;
using FluentAssertions;
using Raven.Core.Services;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Entities;
using Raven.Core.Abstractions.Services;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Tests
{
    public class UsersServiceTests
    {
        private readonly IUsersService _usersService;
        private readonly IUsersRepository _usersRepository;

        public UsersServiceTests()
        {
            _usersRepository = A.Fake<IUsersRepository>();
            _usersService = new UsersService(_usersRepository);
        }

        [Fact]
        public async Task CreateOtpUser_TakesCreateOtpUserRequestObject_ReturnsTupleOfBooleanAndCreateOtpUserResponseAndProblemDetails()
        {
            // Arrange
            CreateOtpUserRequest request = new("johndoe@gmail.com", "Doe", "John", "+1222333444");
            A.CallTo(() => _usersRepository.SaveOtpUser(A<OtpUser>.Ignored)).Returns((true, null));

            // Act
            var result = await _usersService.CreateOtpUser(request);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
