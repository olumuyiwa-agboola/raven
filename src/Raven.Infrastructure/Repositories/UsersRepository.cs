using Raven.Core.Abstractions.Repositories;
using Raven.Core.Entities;

namespace Raven.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of the <see cref="IUsersRepository"/> interface for managing OTP (One-Time Password) users.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        public Task<int> AddOtpUser(OtpUser otpUser)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteOtpUser(OtpUser otpUser)
        {
            throw new NotImplementedException();
        }

        public Task<OtpUser> GetOtpUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateOtpUser(OtpUser otpUser)
        {
            throw new NotImplementedException();
        }
    }
}
