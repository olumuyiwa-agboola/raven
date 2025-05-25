using Raven.Core.Models;
using Raven.Core.Abstractions.Services;

namespace Raven.Core.Services
{
    /// <summary>
    /// Implementation of the <see cref="IOtpsService"/> interface for generating and validating OTPs (One-Time Passwords).
    /// </summary>
    public class OtpsService : IOtpsService
    {
        public Task<(bool, Otp?, Error?)> GenerateOtp(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Error?)> ValidateOtp(string userId, string otpCode)
        {
            throw new NotImplementedException();
        }
    }
}
