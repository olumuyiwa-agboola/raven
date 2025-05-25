using System.Net;
using Raven.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Abstractions.Services;

namespace Raven.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUsersService _usersService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateOtpUserRequest request)
    {
        var (isSiccess, createOtpUserResponse, problemDetails) = await _usersService.CreateOtpUser(request);

        return isSiccess ? StatusCode((int)HttpStatusCode.OK, createOtpUserResponse) : StatusCode((int)problemDetails!.Status!, problemDetails);
    }
}
