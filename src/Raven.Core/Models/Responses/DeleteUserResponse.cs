using Humanizer;

namespace Raven.Core.Models.Responses
{
    /// <summary>
    /// Represents the body of a 200 OK response from a request to delete a user.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data to be returned in the body of a 200 OK 
    /// response from a request to delete a user sent to the relevant endpoint.</remarks>
    public record DeleteUserResponse
    {
        private DeleteUserResponse(string userId, DateTimeOffset now)
        {
            UserId = userId;
            DeletedAt = $"{now.ToString("dddd dd MMMM, yyyy, hh:mm:ss tt")} ({now.Humanize()})";
        }

        /// <summary>
        /// The user identifier associated with the user.
        /// </summary>
        public string? UserId { get; init; }

        /// <summary>
        /// The date and time the user was deleted.
        /// </summary>
        public string? DeletedAt { get; init; }

        /// <summary>
        /// Creates a new instance of the <see cref="DeleteUserResponse"/> class using the unique identifer
        /// of the deleted user.
        /// </summary>
        /// <param name="userId">The generated unique identifier of the new user. Cannot be null.</param>
        /// <returns>A <see cref="DeleteUserResponse"/> instance containing the unique identifier and the 
        /// <see langword="DateTimeOffSet"/> of deletion of the user.</returns>
        public static DeleteUserResponse Create(string userId)
            => new(userId, DateTimeOffset.Now);
    }
}
