using Microsoft.AspNetCore.Mvc;
using Raven.Core.Libraries.Enums;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Responses;

namespace Raven.Core.Abstractions.Services;

/// <summary>
/// Defines a contract for managing users, including operations for retrieval, creation, 
/// updating, and deletion of user records.
/// </summary>
/// <remarks>This service provides methods to interact with user data, such as retrieving user
/// details, creating new users, updating existing user information, and deleting user records.</remarks>
public interface IUsersService
{
    /// <summary>
    /// Retrieves the user associated with the specified search parameter.
    /// </summary>
    /// <remarks>Use this method to retrieve the details of a specific user using one of the available <see cref="SearchType"/>. Ensure that
    /// the provided <paramref name="searchParameter"/> and <paramref name="searchType"/> are valid and correspond to an existing user in the
    /// system.</remarks>
    /// <param name="searchParameter">The unique identifier of the user for whom the user information is to be retrieved. This parameter
    /// cannot be null or empty.</param>
    /// <param name="searchType">Specifies the type of search to be performed. This parameter cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
    /// <list type="number">
    ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
    ///     <item>a <see cref="GetUserResponse"/> object representing the response object to be returned to the caller if no error occured, and</item>
    ///     <item>a <see cref="ProblemDetails"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
    /// </list>    
    /// </returns>
    Task<(bool, GetUserResponse?, ProblemDetails?)> GetUser(string searchParameter, SearchType searchType);

    /// <summary>
    /// Deletes the user associated with the specified user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to delete. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
    /// <list type="number">
    ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
    ///     <item>a <see cref="ProblemDetails"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
    /// </list>    
    /// </returns>
    Task<(bool, ProblemDetails?)> DeleteUser(string userId);

    /// <summary>
    /// Creates a new user with the specified details.
    /// </summary>
    /// <remarks>This method performs validation on the input parameters and may fail if the provided
    /// details are invalid or if the user already exists. Ensure that all required fields are properly formatted
    /// before calling this method.</remarks>
    /// <param name="request">The <see cref="CreateUserRequest"/> object which holds the first name, last name, 
    /// email address and phone number of the user. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
    /// <list type="number">
    ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
    ///     <item>a <see cref="CreateUserResponse"/> object representing the response object to be returned to the caller if no error occured, and</item>
    ///     <item>a <see cref="ProblemDetails"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
    /// </list>
    /// </returns>
    Task<(bool, CreateUserResponse?, ProblemDetails?)> CreateUser(CreateUserRequest request);

    /// <summary>
    /// Updates the details of a user in the system.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to update. This value cannot be null or empty.</param>
    /// <param name="request">An <see cref="UpdateUserRequest"/> object containing the fields to be updated. At least 
    /// one of these properties must be not be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
    /// <list type="number">
    ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
    ///     <item>a <see cref="ProblemDetails"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
    /// </list>
    /// </returns>
    Task<(bool, ProblemDetails?)> UpdateUser(string userId, UpdateUserRequest request);
}
