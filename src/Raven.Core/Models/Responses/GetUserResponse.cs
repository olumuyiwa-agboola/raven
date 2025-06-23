using Humanizer;
using System.ComponentModel;
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

        [Description("The first name of the user.")]
        public string FirstName { get; init; }

        [Description("The last name of the user.")]
        public string LastName { get; init; }

        [Description("The system-assigned ID of the user.")]
        public string UserId { get; init; }

        [Description("The email address associated with the user.")]
        public string EmailAddress { get; init; }

        [Description("The phone number associated with the user.")]
        public string PhoneNumber { get; init; }

        [Description("The date and time the user was created, formatted as 'dddd dd MMMM, yyyy, hh:mm:ss tt' with a humanized duration since creation.")]
        public string CreatedAt { get; init; }

        [Description("The date and time the user was last updated, formatted as 'dddd dd MMMM, yyyy, hh:mm:ss tt' with a humanized duration since last update.")]
        public string LastUpdatedAt { get; init; }

        /// <summary>
        /// Creates a new instance of the <see cref="GetUserResponse"/> class from a 
        /// <see cref="User"/> object.
        /// </summary>
        /// <param name="user">The user object to initialize the response with. Cannot be null.</param>
        /// <returns>A <see cref="GetUserResponse"/> instance containing details of the specified user
        /// to be returned as part of the response to a request to retrieve user details.</returns>
        public static GetUserResponse Create(User user)
            => new(user);
    }
}
