using System.Net;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Requests;
using Raven.Core.Abstractions.Services;

namespace Raven.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUsersService _usersService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var (isSuccess, createUserResponse, problemDetails) = await _usersService.CreateUser(request);

        return isSuccess ? 
            StatusCode((int)HttpStatusCode.OK, createUserResponse) : 
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }
}
