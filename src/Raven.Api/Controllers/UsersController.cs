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

    [HttpGet]
    public async Task<IActionResult> GetUser(GetUserRequest request)
    {
        var (isSuccess, getUserResponse, problemDetails) = await _usersService.GetUser(request.SearchParameter, request.SearchType);

        return isSuccess ?
            StatusCode((int)HttpStatusCode.OK, getUserResponse) :
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }

    [HttpDelete]
    [Route("{userId}")]
    public async Task<IActionResult> DeleteUser(UserIdRouteParameter routeParameter)
    {
        var (isSuccess, deleteUserResponse, problemDetails) = await _usersService.DeleteUser(routeParameter.UserId);

        return isSuccess ?
            StatusCode((int)HttpStatusCode.OK, deleteUserResponse) :
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }

    [HttpPut]
    [Route("{userId}")]
    public async Task<IActionResult> UpdateUser(UserIdRouteParameter routeParameter, UpdateUserRequest request)
    {
        var (isSuccess, updateUserResponse, problemDetails) = await _usersService.UpdateUser(routeParameter.UserId, request);

        return isSuccess ?
            StatusCode((int)HttpStatusCode.OK, updateUserResponse) :
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }
}
