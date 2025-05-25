using Raven.Core.Models;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Implementation of the <see cref="IOtpsRepository"/> interface for managing OTP (One-Time Password) data in a database.
    /// </summary>
    public class OtpsRepository : IOtpsRepository
    {
        public Task<(bool, Otp?, Error?)> GetOtp(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Error?)> SaveOtp(Otp otp)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Error?)> UpdateOtpStatusToUsed(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Error?)> UpdateOtpTryCount(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
