using Raven.Core.Models.Entities;
using Raven.Core.Models.Shared;

namespace Raven.Core.Abstractions.Repositories
{
    /// <summary>
    /// Defines a contract for managing OTP (One-Time Password) user data in a database.
    /// </summary>
    /// <remarks>This interface provides methods for adding, updating, deleting, and retrieving records of OTP users. 
    /// Implementations of this interface are responsible for handling the persistence and retrieval of <see cref="OtpUser"/> objects.</remarks>
    public interface IUsersRepository
    {
        /// <summary>
        /// Adds a new OTP (One-Time Password) user to the system by saving the details of the user in a database.
        /// </summary>
        /// <remarks>The <paramref name="otpUser"/> parameter should contain all required information for
        /// the user to be successfully added. Ensure that the provided data meets any validation requirements defined
        /// by the system.</remarks>
        /// <param name="otpUser">The OTP user to be added. This parameter must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> SaveOtpUser(OtpUser otpUser);

        /// <summary>
        /// Deletes the specified OTP user from the system by removing the details of the user from a database.
        /// </summary>
        /// <param name="otpUser">The OTP user to be deleted. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> DeleteOtpUser(string userId);

        /// <summary>
        /// Updates the details of an existing OTP user in the system by updating the details of the user in a database.
        /// </summary>
        /// <remarks>Use this method to modify the details of an OTP user. Ensure that the provided <see
        /// cref="OtpUser"/> object contains valid and complete information before calling this method.</remarks>
        /// <param name="otpUser">The <see cref="OtpUser"/> object containing the updated user details. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> UpdateOtpUser(OtpUser otpUser);

        /// <summary>
        /// Retrieves the OTP (One-Time Password) user associated with the specified user identifier.
        /// </summary>
        /// <remarks>Use this method to retrieve OTP-related information for a specific user. Ensure that
        /// the provided  <paramref name="userId"/> is valid and corresponds to an existing user in the
        /// system.</remarks>
        /// <param name="userId">The unique identifier of the user for whom the OTP user information is to be retrieved. This parameter
        /// cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="OtpUser"/> object holding the details of the OTP user, or <see langword="null"/> if no user is found, and</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, OtpUser?, Error?)> GetOtpUser(string userId);
    }
}
