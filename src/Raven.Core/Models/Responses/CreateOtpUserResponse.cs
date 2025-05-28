using Humanizer;

namespace Raven.Core.Models.Responses
{
    /// <summary>
    /// Represents the body of a 200 OK response from a request to create a new OTP user.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data to be returned in the body of a 200 OK 
    /// response from a request to create a new OTP user sent to the relevant endpoint.</remarks>
    public record CreateOtpUserResponse
    {
        private CreateOtpUserResponse() { }

        private CreateOtpUserResponse(string userId, DateTimeOffset createdAt)
        {
            UserId = userId;
            Message = "OTP user created successfully!";
            CreatedAt = $"{createdAt.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt")} ({createdAt.Humanize()})";
        }

        /// <summary>
        /// The response message indicating a successful creation of the user.
        /// </summary>
        public string? Message { get; init; }

        /// <summary>
        /// Gets or initializes the user identifier associated with the user.
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// Gets or initializes the date and time the user was created.
        /// </summary>
        public string? CreatedAt { get; init; }

        public static CreateOtpUserResponse Create(string userId, DateTimeOffset createdAt)
            => new(userId, createdAt);
    }
}
