using Raven.Core.Abstractions.Services;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Shared;

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
