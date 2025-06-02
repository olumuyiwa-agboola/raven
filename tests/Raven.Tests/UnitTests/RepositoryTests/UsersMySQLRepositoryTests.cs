using Bogus;
using FakeItEasy;
using Raven.Core.Models.Entities;
using Raven.Core.Abstractions.Factories;
using Raven.Infrastructure.Repositories.MySQL;

namespace Raven.Tests.UnitTests.RepositoryTests
{
    public class UsersMySQLRepositoryTests
    {
        private readonly OtpUser _sampleOtpUser;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly UsersMySQLRepository _usersMySQLRepository;

        public UsersMySQLRepositoryTests()
        {
            _sampleOtpUser = new Faker<OtpUser>(locale: "en_NG")
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.CreatedAt, _ => DateTimeOffset.Now)
                .RuleFor(x => x.LastUpdatedAt, _ => DateTimeOffset.Now)
                .RuleFor(x => x.UserId, _ => Ulid.NewUlid().ToString())
                .RuleFor(x => x.PhoneNumber, x => x.Phone.PhoneNumber())
                .RuleFor(x => x.EmailAddress, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName));

            _dbConnectionFactory = A.Fake<IDbConnectionFactory>();
            _usersMySQLRepository = new UsersMySQLRepository(_dbConnectionFactory);
        }

        [Fact]
        public async Task SaveOtpUser_TakesOtpUserObject_ReturnsTupleOfTrueAndNull()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
