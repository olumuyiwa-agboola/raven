using Raven.Core.Entities;

namespace Raven.Core.Abstractions.Repositories
{
    /// <summary>
    /// Defines a contract for managing OTP (One-Time Password) user data in a database.
    /// </summary>
    /// <remarks>This interface provides methods for adding, updating, deleting, and retrieving records of OTP users. 
    /// Implementations of this interface are responsible for handling the persistence and retrieval of <see cref="OtpUser"/> objects.</remarks>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new OTP (One-Time Password) user to the system by saving the details of the user in a database.
        /// </summary>
        /// <remarks>The <paramref name="otpUser"/> parameter should contain all required information for
        /// the user to be successfully added. Ensure that the provided data meets any validation requirements defined
        /// by the system.</remarks>
        /// <param name="otpUser">The OTP user to be added. This parameter must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an integer indicating the
        /// number of records saved to the database, which is 1 if the operation is successful.</returns>
        Task<int> AddOtpUser(OtpUser otpUser);

        /// <summary>
        /// Deletes the specified OTP user from the system by removing the details of the user from a database.
        /// </summary>
        /// <param name="otpUser">The OTP user to be deleted. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of records affected
        /// by the deletion, which is 1 if the operation is successful.</returns>
        Task<int> DeleteOtpUser(OtpUser otpUser);

        /// <summary>
        /// Updates the details of an existing OTP user in the system by updating the details of the user in a database.
        /// </summary>
        /// <remarks>Use this method to modify the details of an OTP user. Ensure that the provided <see
        /// cref="OtpUser"/> object contains valid and complete information before calling this method.</remarks>
        /// <param name="otpUser">The <see cref="OtpUser"/> object containing the updated user details. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of records affected
        /// by the update, which is 1 if the operation is successful.</returns>
        Task<int> UpdateOtpUser(OtpUser otpUser);

        /// <summary>
        /// Retrieves the OTP (One-Time Password) user associated with the specified user identifier.
        /// </summary>
        /// <remarks>Use this method to retrieve OTP-related information for a specific user. Ensure that
        /// the provided  <paramref name="userId"/> is valid and corresponds to an existing user in the
        /// system.</remarks>
        /// <param name="userId">The unique identifier of the user for whom the OTP user information is to be retrieved. This parameter
        /// cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OtpUser"/> 
        /// associated with the specified user identifier, or <see langword="null"/> if no such user exists.</returns>
        Task<OtpUser> GetOtpUser(string userId);
    }
}
