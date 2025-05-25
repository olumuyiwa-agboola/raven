using Raven.Core.Models;
using Raven.Core.Abstractions.Services;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Core.Services
{
    /// <summary>
    /// Implementation of the <see cref="IUsersService"/> interface for managing OTP (One-Time Password) users.
    /// </summary>
    public class UsersService(IUsersRepository _usersRepo) : IUsersService
    {
        public async Task<(bool, OtpUser?, Error?)> CreateOtpUser(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            var otpUser = OtpUser.Create(firstName, lastName, emailAddress, phoneNumber);

            var (isSuccess, error) = await _usersRepo.SaveOtpUser(otpUser);
            
            return isSuccess ? (true, otpUser, null) : (false, null, error);
        }

        public Task<(bool, Error?)> DeleteOtpUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, OtpUser?, Error?)> GetOtpUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, Error?)> UpdateOtpUser(string userId, string? firstName, string? lastName, string? emailAddress, string? phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
