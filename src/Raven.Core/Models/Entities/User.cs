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
        /// The email address associated with the user.
        /// </summary>
        public string EmailAddress { get; init; } = string.Empty;

        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        public string UserId { get; init; } = string.Empty;

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; init; } = string.Empty;

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// The phone number associated with the user.
        /// </summary>
        public string PhoneNumber { get; init; } = string.Empty;

        /// <summary>
        /// The date and time when the user was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// The date and time when the user was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; init; }

        /// <summary>
        /// Creates a new instance of the <see cref="User"/> class with the specified details.
        /// </summary>
        /// <remarks>The <see cref="User"/> instance is automatically assigned a unique identifier and
        /// timestamps for creation and last update.</remarks>
        /// <param name="lastName">The last name of the user. Cannot be null or empty.</param>
        /// <param name="firstName">The first name of the user. Cannot be null or empty.</param>
        /// <param name="phoneNumber">The phone number of the user. Cannot be null or empty.</param>
        /// <param name="emailAddress">The email address of the user. Must be a valid email format.</param>
        /// <returns>A new <see cref="User"/> instance initialized with the provided details.</returns>
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
