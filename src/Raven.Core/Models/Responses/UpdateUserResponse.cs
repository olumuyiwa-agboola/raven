using Humanizer;

namespace Raven.Core.Models.Responses
{
    /// <summary>
    /// Represents the body of a 200 OK response from a request to update a user.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data to be returned in the body of a 200 OK 
    /// response from a request to update a user sent to the relevant endpoint.</remarks>
    public record UpdateUserResponse
    {
        private UpdateUserResponse(string userId, DateTimeOffset now)
        {
            UserId = userId;
            LastUpdatedAt = $"{now.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt")} ({now.Humanize()})";
        }

        /// <summary>
        /// Gets or initializes the user identifier associated with the user.
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// Gets or initializes the date and time the user was last updated.
        /// </summary>
        public string? LastUpdatedAt { get; init; }

        public static UpdateUserResponse Create(string userId)
            => new(userId, DateTimeOffset.Now);
    }
}
