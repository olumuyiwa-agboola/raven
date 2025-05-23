using Raven.Core.Entities;

namespace Raven.Core.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for managing OTP (One-Time Password) users, including operations for retrieval, creation, 
    /// updating, and deletion of user records.
    /// </summary>
    /// <remarks>This service provides methods to interact with OTP user data, such as retrieving user
    /// details,  creating new users, updating existing user information, and deleting user records.  Each method
    /// operates asynchronously and returns results or status codes indicating the outcome of the operation.</remarks>
    public interface IUsersService
    {
        /// <summary>
        /// Retrieves the OTP (One-Time Password) user associated with the specified user identifier.
        /// </summary>
        /// <remarks>This method is used to retrieve OTP-related information for a user.
        /// Ensure that the provided <paramref name="userId"/> is valid  and corresponds to a profiled user.</remarks>
        /// <param name="userId">The unique identifier of the user for whom the OTP user information is to be retrieved. 
        /// Must not be <see langword="null"/> or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OtpUser"/> 
        /// associated with the specified user identifier, or <see langword="null"/> if no such user exists.</returns>
        Task<OtpUser?> GetOtpUser(string userId);

        /// <summary>
        /// Deletes the OTP (One-Time Password) user associated with the specified user identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete. Cannot be null or empty.</param>
        /// <returns>The number of records deleted as an <see cref="int"/> if the operation is successful;  otherwise, <see
        /// langword="null"/> if the user does not exist or the operation fails.</returns>
        Task<int?> DeleteOtpUser(string userId);

        /// <summary>
        /// Creates a new OTP (One-Time Password) user with the specified details.
        /// </summary>
        /// <remarks>This method performs validation on the input parameters and may fail if the provided
        /// details are invalid or if the user already exists. Ensure that all required fields are properly formatted
        /// before calling this method.</remarks>
        /// <param name="firstName">The first name of the user. Cannot be null or empty.</param>
        /// <param name="lastName">The last name of the user. Cannot be null or empty.</param>
        /// <param name="emailAddress">The email address of the user. Must be a valid email format and cannot be null or empty.</param>
        /// <param name="phoneNumber">The phone number of the user. Must be a valid phone number and cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unique identifier of the
        /// created user, or <see langword="null"/> if the user could not be created.</returns>
        Task<string?> CreateOtpUser(string firstName, string lastName, string emailAddress, string phoneNumber);
    
        /// <summary>
        /// Updates the details of an OTP user in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to update. This value cannot be null or empty.</param>
        /// <param name="firstName">The updated first name of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <param name="lastName">The updated last name of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <param name="emailAddress">The updated email address of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <param name="phoneNumber">The updated phone number of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of records updated, 
        /// or <see langword="null"/> if the user was not found.</returns>
        Task<int?> UpdateOtpUser(string userId, string? firstName, string? lastName, string? emailAddress, string? phoneNumber);
    }
}
