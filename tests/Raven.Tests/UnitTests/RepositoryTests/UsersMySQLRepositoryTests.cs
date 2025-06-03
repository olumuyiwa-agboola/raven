using Bogus;
using FluentAssertions;
using Raven.Core.Models.Entities;
using Raven.Core.Abstractions.Factories;
using Raven.Infrastructure.Repositories.MySQL;
using Microsoft.Extensions.DependencyInjection;

namespace Raven.Tests.UnitTests.RepositoryTests
{
    public class UsersMySQLRepositoryTests : IClassFixture<RavenMySQLDbFixture>
    {
        private readonly OtpUser _sampleOtpUser;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly UsersMySQLRepository _usersMySQLRepository;

        public UsersMySQLRepositoryTests(RavenMySQLDbFixture _ravenMySQLDbFixture)
        {
            _sampleOtpUser = new Faker<OtpUser>(locale: "en_NG")
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.CreatedAt, _ => DateTimeOffset.Now)
                .RuleFor(x => x.LastUpdatedAt, _ => DateTimeOffset.Now)
                .RuleFor(x => x.UserId, _ => Ulid.NewUlid().ToString())
                .RuleFor(x => x.PhoneNumber, x => x.Phone.PhoneNumber())
                .RuleFor(x => x.EmailAddress, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName));

            _dbConnectionFactory = _ravenMySQLDbFixture.Services!.GetRequiredService<IDbConnectionFactory>();
            _usersMySQLRepository = new UsersMySQLRepository(_dbConnectionFactory);
        }

        [Fact]
        public async Task SaveOtpUser_TakesOtpUserObject_ReturnsTupleOfTrueAndNull()
        {
            var (isSavedSuccessfully, error) = await _usersMySQLRepository.SaveOtpUser(_sampleOtpUser);

            isSavedSuccessfully.Should().BeTrue();
            error.Should().BeNull();
        }
    }
}
