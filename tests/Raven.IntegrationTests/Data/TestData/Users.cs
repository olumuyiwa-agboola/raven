using Bogus;
using Raven.Core.Models.Entities;

namespace Raven.IntegrationTests.Data.TestData
{
    public static class Users
    {
        public static List<User> Generate(int numberOfUsers)
        {
            var userFaker = new Faker<User>(locale: "en_NG")
                .RuleFor(user => user.LastName, faker => faker.Name.LastName())
                .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
                .RuleFor(user => user.CreatedAt, _ => DateTimeOffset.Now)
                .RuleFor(user => user.LastUpdatedAt, _ => DateTimeOffset.Now)
                .RuleFor(user => user.PhoneNumber, faker =>
                {
                    string phoneNumber;
                    var usedPhoneNumbers = new HashSet<string>();

                    do
                    {
                        phoneNumber = faker.Phone.PhoneNumber();
                    } while (!usedPhoneNumbers.Add(phoneNumber));

                    return phoneNumber;
                })
                .RuleFor(user => user.EmailAddress, (faker, user) =>
                {
                    string emailAddress;
                    var usedEmailAddresses = new HashSet<string>();

                    do
                    {
                        emailAddress = faker.Internet.Email(user.FirstName, user.LastName);
                    } while (!usedEmailAddresses.Add(emailAddress));

                    return emailAddress;
                })
                .RuleFor(user => user.UserId, faker =>
                {
                    string userId;
                    var usedUserIds = new HashSet<string>();

                    do
                    {
                        userId = Ulid.NewUlid().ToString();
                    } while (!usedUserIds.Add(userId));

                    return userId;
                });

            return userFaker.Generate(numberOfUsers);
        }
    }
}