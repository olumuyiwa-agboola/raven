namespace Raven.Core.Models.Entities
{
    /// <summary>
    /// Represents a user that has been profiled for authorization code verification.
    /// </summary>
    /// <remarks>This class encapsulates the essential details of a user, including their name,
    /// contact information and identifier, for use in authorization-related operations or authentication
    /// workflows.</remarks>
    public record User
    {
        /// <summary>
        /// Gets or initializes the email address associated with the user.
        /// </summary>
        public string EmailAddress { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the unique identifier for the user.
        /// </summary>
        public string UserId { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the last name of the user.
        /// </summary>
        public string LastName { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the first name of the user.
        /// </summary>
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the phone number associated with the user.
        /// </summary>
        public string PhoneNumber { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the date and time when the user was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Gets or initializes the date and time when the user was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; init; }

        public static User Create(string firstName, string lastName, string emailAddress, string phoneNumber)
            => new() 
            {
                LastName = lastName,
                FirstName = firstName,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress,
                CreatedAt = DateTimeOffset.Now,
                LastUpdatedAt = DateTimeOffset.Now,
                UserId = Ulid.NewUlid().ToString()
            };
    }
}
