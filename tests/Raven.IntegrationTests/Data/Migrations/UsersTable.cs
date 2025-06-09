using FluentMigrator;
using Raven.Core.Models.Entities;
using Raven.IntegrationTests.Data.TestData;

namespace Raven.IntegrationTests.Data.Migrations
{
    [Migration(1)]
    public class UsersTable : Migration
    {
        public static List<User> SeedData = Users.Generate(10);

        public override void Up()
        {
            Create.Table("otp_users")
                .WithColumn("user_id").AsString(50).PrimaryKey()
                .WithColumn("first_name").AsString(100).NotNullable()
                .WithColumn("last_name").AsString(100).NotNullable()
                .WithColumn("email_address").AsString(200).Indexed()
                .WithColumn("phone_number").AsString(50).Indexed()
                .WithColumn("created_at").AsDateTime()
                .WithColumn("last_updated_at").AsDateTime();


            foreach (var user in SeedData)
            {
                Insert.IntoTable("otp_users").Row(new
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
            Delete.Table("otp_users");
        }
    }
}
