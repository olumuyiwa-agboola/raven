using FluentMigrator;

namespace Raven.Infrastructure.Migrations
{
    [Migration(1)]
    public class CreateOtpUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("otp_users")
                .WithColumn("user_id").AsString(50).PrimaryKey()
                .WithColumn("first_name").AsString(100).Unique().NotNullable()
                .WithColumn("last_name").AsString(100).Unique().NotNullable()
                .WithColumn("email_address").AsString(200).Indexed()
                .WithColumn("phone_number").AsString(50).Indexed()
                .WithColumn("created_at").AsDateTime()
                .WithColumn("last_updated_at").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("otp_users");
        }
    }
}
