using Raven.Core.Factories;
using Raven.Core.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Responses;
using Raven.Core.Abstractions.Services;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Core.Services
{
    /// <summary>
    /// Implementation of the <see cref="IUsersService"/> interface for managing OTP (One-Time Password) users.
    /// </summary>
    public class UsersService(IUsersRepository _usersRepo) : IUsersService
    {
        public async Task<(bool, CreateOtpUserResponse?, ProblemDetails?)> CreateOtpUser(CreateOtpUserRequest request)
        {
            var otpUser = OtpUser.Create(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

            var (otpUserWasSavedSuccessfully, error) = await _usersRepo.SaveOtpUser(otpUser);

            if (otpUserWasSavedSuccessfully)
                return (true, CreateOtpUserResponse.Create(otpUser.UserId, otpUser.CreatedAt), null);
            else
                return (false, null, ProblemDetailsFactory.CreateProblemDetailsFromError(error!));
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
