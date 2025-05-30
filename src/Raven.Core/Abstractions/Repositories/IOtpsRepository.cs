﻿using Raven.Core.Models.Entities;
using Raven.Core.Models.Shared;

namespace Raven.Core.Abstractions.Repositories
{
    /// <summary>
    /// Defines methods for managing and validating one-time passwords (OTPs) in a database.
    /// </summary>
    /// <remarks>This interface provides functionality to save OTPs, update their status, track the number of 
    /// validation attempts, and OTP-related data for a specific user. Implementations of this interface are
    /// responsible for handling the persistence and retrieval of OTP-related data.</remarks>
    public interface IOtpsRepository
    {
        /// <summary>
        /// Saves the specified OTP (One-Time Password) to the database.
        /// </summary>
        /// <param name="otp">The <see cref="Otp"/> object containing the OTP details to be saved. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> SaveOtp(Otp otp);

        /// <summary>
        /// Updates the status of the OTP (One-Time Password) associated with the specified user to "USED."
        /// </summary>
        /// <remarks>This method is typically used to mark an OTP as consumed after it has been
        /// successfully validated. Ensure that the provided <paramref name="userId"/> corresponds to a valid user in
        /// the system.</remarks>
        /// <param name="userId">The unique identifier of the user whose OTP status is to be updated. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> UpdateOtpStatusToUsed(string userId);

        /// <summary>
        /// Updates the OTP try count for the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose OTP try count is to be updated. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> UpdateOtpTryCount(string userId);

        /// <summary>
        /// Retrieves the saved OTP (One-Time Password) for the specified user.
        /// </summary>
        /// <remarks>This method is used to fetch the OTP details for a specific user. Ensure that the provided
        /// <paramref name="userId"/> is valid and corresponds to an existing user in the system.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Otp"/> object holding the details of the OTP, or <see langword="null"/> if no OTP is found, and</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Otp?, Error?)> GetOtp(string userId);
    }
}
