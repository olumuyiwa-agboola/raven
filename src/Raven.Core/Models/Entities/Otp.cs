using Raven.Core.Libraries.Enums;

namespace Raven.Core.Models.Entities
{
    /// <summary>
    /// Represents a one-time password (OTP) generated for a user, including its state, usage, and expiration details.
    /// </summary>
    /// <remarks>This class is used to store and manage information about an OTP, such as its status,
    /// the user it was generated for, the OTP code itself, the number of attempts made to use it, and its
    /// generation and expiration timestamps.</remarks>
    public class Otp
    {
        /// <summary>
        /// Gets or sets the current status of the OTP (One-Time Password).
        /// </summary>
        public required OtpStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Gets or sets the one-time password (OTP) code used for authorization or verification purposes.
        /// </summary>
        public required string OtpCode { get; set; }

        /// <summary>
        /// Gets or sets the number of verification attempts that has been made on the OTP.
        /// </summary>
        public required int NumberOfTries { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the OTP.
        /// </summary>
        public required DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time the OTP was generated.
        /// </summary>
        public required DateTime GeneratedAt { get; set; }
    }
}
