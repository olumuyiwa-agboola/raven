using System.Net;
using Raven.Core.Enums;
using Raven.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Abstractions.Services;

namespace Raven.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUsersService _usersService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var (otpUserCreationIsSuccessful, otpUser, otpUserCreationError) = await _usersService.CreateOtpUser(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

        switch (otpUserCreationIsSuccessful)
        {
            case true:
                return StatusCode((int)HttpStatusCode.OK, otpUser);

            case false:
                return (otpUserCreationError?.Type) switch
                {
                    ErrorType.NotFound => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    ErrorType.Exception => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    ErrorType.InvalidInput => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    ErrorType.Unauthorized => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    ErrorType.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    ErrorType.DatabaseInsertError => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    ErrorType.DatabaseInsertFailure => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError, otpUserCreationError),
                };
        }
    }
}
