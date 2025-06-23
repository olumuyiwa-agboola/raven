using System.ComponentModel;

namespace Raven.Core.Models.Responses
{
    /// <summary>
    /// Represents the body of a 200 OK response from a request to create a new user.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data to be returned in the body of a 200 OK 
    /// response from a request to create a new user sent to the relevant endpoint.</remarks>
    public record CreateUserResponse
    {
        private CreateUserResponse(string userId, DateTimeOffset createdAt)
        {
            UserId = userId;
            CreatedAt = $"{createdAt.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt")}";
        }

        [Description("The system-assigned ID of the user.")]
        public string? UserId { get; init; }

        [Description("The date and time the user was created, formatted as 'dddd dd MMMM, yyyy, hh:mm:ss tt' (e.g., 'Monday 01 January, 2024, 12:00:00 AM').")]
        public string? CreatedAt { get; init; }

        /// <summary>
        /// Creates a new instance of the <see cref="CreateUserResponse"/> class using the generate unique 
        /// identifier of the user and the date and time when the user was created.
        /// </summary>
        /// <param name="userId">The generated unique identifier of the new user. Cannot be null.</param>
        /// <param name="createdAt">The <see langword="DateTimeOffSet"/> the user was created at. Cannot be null.</param>
        /// <returns>A <see cref="CreateUserResponse"/> instance containing the unique identifier and the <see langword="DateTimeOffSet"/>
        /// of creation of the new user.</returns>
        public static CreateUserResponse Create(string userId, DateTimeOffset createdAt)
            => new(userId, createdAt);
    }
}
