using FluentMigrator;
using Raven.Core.Models.Entities;
using Raven.Core.Libraries.Constants;
using Raven.IntegrationTests.Data.TestData;

namespace Raven.IntegrationTests.Data.Migrations
{
    [Migration(1)]
    public class UsersTable : Migration
    {
        public override void Up()
        {
            Create.Table($"{DataStores.Users.Name}")
                .WithColumn($"{DataStores.Users.Attributes.UserId}").AsString(50).PrimaryKey()
                .WithColumn($"{DataStores.Users.Attributes.FirstName}").AsString(100).NotNullable()
                .WithColumn($"{DataStores.Users.Attributes.LastName}").AsString(100).NotNullable()
                .WithColumn($"{DataStores.Users.Attributes.EmailAddress}").AsString(200).Indexed()
                .WithColumn($"{DataStores.Users.Attributes.PhoneNumber}").AsString(50).Indexed()
                .WithColumn($"{DataStores.Users.Attributes.CreatedAt}").AsDateTime()
                .WithColumn($"{DataStores.Users.Attributes.LastUpdatedAt}").AsDateTime();

            List<User> users = Users.Generate(10);

            foreach (var user in users)
            {
                Insert.IntoTable($"{DataStores.Users.Name}").Row(new
                {
                    user_id = user.UserId,
                    last_name = user.LastName,
                    first_name = user.FirstName,
                    email_address = user.EmailAddress,
                    phone_number = user.PhoneNumber,
                    created_at = user.CreatedAt,
                    last_updated_at = user.LastUpdatedAt,
                });
            }
        }

        public override void Down()
        {
            Delete.Table($"{DataStores.Users.Name}");
        }
    }
}
