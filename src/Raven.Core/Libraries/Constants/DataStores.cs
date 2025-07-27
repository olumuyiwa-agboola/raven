namespace Raven.Core.Libraries.Constants;

public static class DataStores
{
    public static class Users 
    { 
        public const string Name = "users";

        public static class Attributes
        {
            public const string UserId = "user_id";

            public const string LastName = "last_name";

            public const string FirstName = "first_name";

            public const string CreatedAt = "created_at";

            public const string PhoneNumber = "phone_number";

            public const string EmailAddress = "email_address";

            public const string LastUpdatedAt = "last_updated_at";
        }
    }
}
