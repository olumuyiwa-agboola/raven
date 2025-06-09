using Raven.Core.Factories;
using Raven.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Entities;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Responses;
using Raven.Core.Abstractions.Services;
using Raven.Core.Abstractions.Repositories;

namespace Raven.Core.Services
{
    /// <summary>
    /// Implementation of the <see cref="IUsersService"/> interface for managing  (One-Time Password) users.
    /// </summary>
    public class UsersService(IUsersRepository _usersRepo) : IUsersService
    {
        public async Task<(bool, CreateUserResponse?, ProblemDetails?)> CreateUser(CreateUserRequest request)
        {
            var user = User.Create(request.FirstName, request.LastName, request.EmailAddress, request.PhoneNumber);

            var (userWasSavedSuccessfully, error) = await _usersRepo.SaveUser(user);

            if (userWasSavedSuccessfully)
                return (true, CreateUserResponse.Create(user.UserId, user.CreatedAt), null);
            else
                return (false, null, ProblemDetailsFactory.CreateProblemDetailsFromError(error!));
        }

        public async Task<(bool, DeleteUserResponse?, ProblemDetails?)> DeleteUser(string userId)
        {
            var (userWasDeletedSuccessfully, error) = await _usersRepo.DeleteUser(userId);

            if (userWasDeletedSuccessfully)
                return (true, DeleteUserResponse.Create(userId), null);
            else
                return (false, null, ProblemDetailsFactory.CreateProblemDetailsFromError(error!));
        }

        public async Task<(bool, GetUserResponse?, ProblemDetails?)> GetUser(string userId)
        {
            var (userWasRetrievedSuccessfully, user, error) = await _usersRepo.GetUser(userId);

            if (userWasRetrievedSuccessfully)
                return (true, GetUserResponse.Create(user!), null);
            else
                return (false, null, ProblemDetailsFactory.CreateProblemDetailsFromError(error!));
        }

        public async Task<(bool, UpdateUserResponse?, ProblemDetails?)> UpdateUser(string userId, UpdateUserRequest request)
        {
            var updates = new UserUpdateDto(request.FirstName, request.LastName, request.PhoneNumber, request.EmailAddress);

            var (userWasUpdatedSuccessfully, error) = await _usersRepo.UpdateUser(userId, updates);

            if (userWasUpdatedSuccessfully)
                return (true, UpdateUserResponse.Create(userId), null);
            else
                return (false, null, ProblemDetailsFactory.CreateProblemDetailsFromError(error!));
        }
    }
}
