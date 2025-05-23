using Raven.Core.Entities;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the <see cref="IOtpsRepository"/> interface for managing OTP (One-Time Password) data.
    /// </summary>
    public class OtpsRepository : IOtpsRepository
    {
        public Task<Otp> GetOtp(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveOtp(Otp otp)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateOtpStatusToUsed(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateOtpTryCount(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
