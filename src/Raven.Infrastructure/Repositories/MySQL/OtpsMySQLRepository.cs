using Raven.Core.Abstractions.Repositories;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Shared;

namespace Raven.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Implementation of the <see cref="IOtpsRepository"/> interface for managing OTP (One-Time Password) data in a database.
    /// </summary>
    public class OtpsMySQLRepository : IOtpsRepository
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
