using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Responses;
using Raven.Core.Models.Shared;

namespace Raven.Core.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for managing OTP (One-Time Password) users, including operations for retrieval, creation, 
    /// updating, and deletion of user records.
    /// </summary>
    /// <remarks>This service provides methods to interact with OTP user data, such as retrieving user
    /// details, creating new users, updating existing user information, and deleting user records.</remarks>
    public interface IUsersService
    {
        /// <summary>
        /// Retrieves the OTP (One-Time Password) user associated with the specified user identifier.
        /// </summary>
        /// <remarks>This method is used to retrieve OTP-related information for a user.
        /// Ensure that the provided <paramref name="userId"/> is valid  and corresponds to a profiled user.</remarks>
        /// <param name="userId">The unique identifier of the user for whom the OTP user information is to be retrieved. 
        /// Must not be <see langword="null"/> or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="User"/> object holding the details of the OTP user, or <see langword="null"/> if the user is not found, and</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, User?, Error?)> GetOtpUser(string userId);

        /// <summary>
        /// Deletes the OTP (One-Time Password) user associated with the specified user identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> DeleteOtpUser(string userId);

        /// <summary>
        /// Creates a new OTP (One-Time Password) user with the specified details.
        /// </summary>
        /// <remarks>This method performs validation on the input parameters and may fail if the provided
        /// details are invalid or if the user already exists. Ensure that all required fields are properly formatted
        /// before calling this method.</remarks>
        /// <param name="request">The <see cref="CreateUserRequest"/> object which holds the first name, last name, 
        /// email address and phone number of the user. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="CreateOtpUserResponse"/> object holding the response data to be returned to the caller, or <see langword="null"/> if an error occured, and</item>
        ///     <item>an <see cref="ProblemDetails"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>
        /// </returns>
        Task<(bool, CreateOtpUserResponse?, ProblemDetails?)> CreateUser(CreateUserRequest request);

        /// <summary>
        /// Updates the details of an OTP user in the system.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to update. This value cannot be null or empty.</param>
        /// <param name="firstName">The updated first name of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <param name="lastName">The updated last name of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <param name="emailAddress">The updated email address of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <param name="phoneNumber">The updated phone number of the user, or <see langword="null"/> to leave it unchanged.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>
        /// </returns>
        Task<(bool, Error?)> UpdateOtpUser(string userId, string? firstName, string? lastName, string? emailAddress, string? phoneNumber);
    }
}
