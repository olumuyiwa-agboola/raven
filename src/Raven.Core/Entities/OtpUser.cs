namespace Raven.Core.Entities
{
    /// <summary>
    /// Represents a user that has been profiled for one-time password (OTP) verification.
    /// </summary>
    /// <remarks>This class encapsulates the essential details of a user, including their name,
    /// contact information and identifier, for use in OTP-related operations or authentication
    /// workflows.</remarks>
    public class OtpUser
    {
        /// <summary>
        /// Gets or sets the email address associated with the user.
        /// </summary>
        public required string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the phone number associated with the user.
        /// </summary>
        public required string PhoneNumber { get; set; }
    }
}
