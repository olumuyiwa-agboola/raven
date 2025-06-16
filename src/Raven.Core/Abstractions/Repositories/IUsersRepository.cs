using Raven.Core.Models.DTOs;
using Raven.Core.Models.Shared;
using Raven.Core.Models.Entities;
using Raven.Core.Libraries.Enums;

namespace Raven.Core.Abstractions.Repositories
{
    /// <summary>
    /// Defines a contract for managing user data in a database.
    /// </summary>
    /// <remarks>This interface provides methods for adding, updating, deleting, and retrieving records of users. 
    /// Implementations of this interface are responsible for handling the persistence and retrieval of <see cref="User"/> objects.</remarks>
    public interface IUsersRepository
    {
        /// <summary>
        /// Adds a new user to the system by saving the details of the user in a database.
        /// </summary>
        /// <remarks>The <paramref name="User"/> parameter should contain all required information for
        /// the user to be successfully added. Ensure that the provided data meets any validation requirements defined
        /// by the system.</remarks>
        /// <param name="User">The user to be added. This parameter must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> SaveUser(User user);

        /// <summary>
        /// Deletes the specified user from the system by removing the details of the user from a database.
        /// </summary>
        /// <param name="User">The user to be deleted. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> DeleteUser(string userId);

        /// <summary>
        /// Updates one or more of the details of an existing user in the system by updating the details of the user in a database.
        /// </summary>
        /// <remarks>Use this method to modify the details of an user. Ensure that the user ID provided is valid and that at least of
        /// of first name, last name, email address and phone number is provided.</remarks>
        /// <param name="lastName">The new last name.</param>
        /// <param name="firstName">The new first name.</param>
        /// <param name="phoneNumber">The new phone number.</param>
        /// <param name="emailAddress">The new email address.</param>
        /// <param name="userId">The user ID of the user whole detail is to be updated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> UpdateUser(string userId, UserUpdateDto updates);

        /// <summary>
        /// Retrieves the user associated with the specified search parameter.
        /// </summary>
        /// <remarks>Use this method to retrieve the details of a specific user using one of the available <see cref="SearchParameter"/>. Ensure that
        /// the provided <paramref name="searchParameter"/> and <paramref name="searchType"/> are valid and correspond to an existing user in the
        /// system.</remarks>
        /// <param name="searchParameter">The unique identifier of the user for whom the user information is to be retrieved. This parameter
        /// cannot be null or empty.</param>
        /// <param name="searchType">Specifies the type of search to be performed. This parameter cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="User"/> object holding the details of the user, or <see langword="null"/> if no user is found, and</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, User?, Error?)> GetUser(string searchParameter, SearchParameter searchType);
    }
}
