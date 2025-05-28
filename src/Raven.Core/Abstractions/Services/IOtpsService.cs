using Raven.Core.Models.Entities;
using Raven.Core.Models.Shared;

namespace Raven.Core.Abstractions.Services
{
    /// <summary>
    /// Defines methods for generating and validating one-time passwords (OTPs) for user authentication and/or authorization.
    /// </summary>
    /// <remarks>This service provides functionality to generate OTPs for a specific user and validate them.</remarks>
    public interface IOtpsService
    {
        /// <summary>
        /// Generates a one-time password (OTP) for the specified user.
        /// </summary>
        /// <remarks>The generated OTP is typically used for authentication or verification purposes. 
        /// Ensure that the user identifier provided is valid and corresponds to an existing user in the
        /// system.</remarks>
        /// <param name="userId">The unique identifier of the user for whom the OTP is being generated. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Otp"/> object holding the details of the OTP, or <see langword="null"/> if an error occured, and</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Otp?, Error?)> GenerateOtp(string userId);

        /// <summary>
        /// Validates the provided one-time password (OTP) for the specified user.
        /// </summary>
        /// <remarks>This method checks the validity of the OTP code against the user's stored or expected
        /// value. Ensure that the OTP is generated and provided to the user through a secure channel.</remarks>
        /// <param name="userId">The unique identifier of the user for whom the OTP is being validated. Cannot be null or empty.</param>
        /// <param name="otpCode">The one-time password to validate. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple which consists of:
        /// <list type="number">
        ///     <item>a <see langword="bool"/> indicating whether the operation was successful,</item>
        ///     <item>an <see cref="Error"/> object holding the error information, or <see langword="null"/> if no error occured.</item>
        /// </list>    
        /// </returns>
        Task<(bool, Error?)> ValidateOtp(string userId, string otpCode);
    }
}
