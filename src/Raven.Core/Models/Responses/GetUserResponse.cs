using Humanizer;
using Raven.Core.Models.Entities;

namespace Raven.Core.Models.Responses
{
    /// <summary>
    /// Represents the body of a 200 OK response from a request to get a user.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data to be returned in the body of a 200 OK 
    /// response from a request to get a user sent to the relevant endpoint.</remarks>
    public record GetUserResponse
    {
        private GetUserResponse(User user)
        {
            UserId = user.UserId;
            LastName = user.LastName;
            FirstName = user.FirstName;
            PhoneNumber = user.PhoneNumber;
            EmailAddress = user.EmailAddress;
            CreatedAt = $"{user.CreatedAt.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt")} ({user.CreatedAt.Humanize()})";
            LastUpdatedAt = $"{user.LastUpdatedAt.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt")} ({user.LastUpdatedAt.Humanize()})";
        }

        /// <summary>
        /// Gets or initializes the user identifier associated with the user.
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// Gets or initializes the email address associated with the user.
        /// </summary>
        public string? EmailAddress { get; init; }

        /// <summary>
        /// Gets or initializes the phone number associated with the user.
        /// </summary>
        public string? PhoneNumber { get; init; }

        /// <summary>
        /// Gets or initializes the first name associated with the user.
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// Gets or initializes the last name associated with the user.
        /// </summary>
        public string? LastName { get; init; }

        /// <summary>
        /// Gets or initializes the date and time the user was created.
        /// </summary>
        public string? CreatedAt { get; init; }

        /// <summary>
        /// Gets or initializes the date and time the user was last updated.
        /// </summary>
        public string? LastUpdatedAt { get; init; }

        public static GetUserResponse Create(User user)
            => new(user);
    }
}
