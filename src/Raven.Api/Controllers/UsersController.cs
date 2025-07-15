using System.Net;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Models.Requests;
using Raven.Core.Models.Responses;
using Raven.Core.Abstractions.Services;

namespace Raven.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService _usersService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [EndpointDescription("Creates and saves a new user to the database.")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var (isSuccess, createUserResponse, problemDetails) = await _usersService.CreateUser(request);

        return isSuccess ? 
            StatusCode((int)HttpStatusCode.OK, createUserResponse) : 
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [EndpointDescription("""
        Retrieves a user based on the provided search value, which can be either the 
        user's system-assigned ID, the user's phone number or the user's email address.
        """)]  
    public async Task<IActionResult> GetUser(GetUserRequest request)
    {
        var (isSuccess, getUserResponse, problemDetails) = await _usersService.GetUser(request.Value, request.SearchType);

        return isSuccess ?
            StatusCode((int)HttpStatusCode.OK, getUserResponse) :
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }

    [HttpDelete]
    [Route("{userId}")]
    [ProducesResponseType(typeof(DeleteUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [EndpointDescription("Deletes the user whose system-assigned ID is given from the database.")]
    public async Task<IActionResult> DeleteUser(UserIdRouteParameter routeParameter)
    {
        var (isSuccess, deleteUserResponse, problemDetails) = await _usersService.DeleteUser(routeParameter.UserId);

        return isSuccess ?
            StatusCode((int)HttpStatusCode.OK, deleteUserResponse) :
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }

    [HttpPut]
    [Route("{userId}")]
    [ProducesResponseType(typeof(UpdateUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [EndpointDescription("Updates the details of the user whose system-assigned ID is given in the database.")]
    public async Task<IActionResult> UpdateUser(UserIdRouteParameter routeParameter, UpdateUserRequest request)
    {
        var (isSuccess, updateUserResponse, problemDetails) = await _usersService.UpdateUser(routeParameter.UserId, request);

        return isSuccess ?
            StatusCode((int)HttpStatusCode.OK, updateUserResponse) :
            StatusCode((int)problemDetails!.Status!, problemDetails);
    }
}
