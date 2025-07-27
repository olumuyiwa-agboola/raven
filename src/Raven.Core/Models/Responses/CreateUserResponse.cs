using System.ComponentModel;

namespace Raven.Core.Models.Responses;

/// <summary>
/// Represents the body of a 200 OK response from a request to create a new user.
/// </summary>
/// <remarks>This class is used to encapsulate the data to be returned in the body of a 200 OK 
/// response from a request to create a new user sent to the relevant endpoint.</remarks>
public record CreateUserResponse
{
    /// <summary>
    /// </summary>
    public CreateUserResponse() { }

    private CreateUserResponse(string userId)
    {
        UserId = userId;
    }

    [Description("The system-assigned ID of the user.")]
    public string? UserId { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="CreateUserResponse"/> class using the generate unique 
    /// identifier of the user and the date and time when the user was created.
    /// </summary>
    /// <param name="userId">The generated unique identifier of the new user. Cannot be null.</param>
    /// <returns>A <see cref="CreateUserResponse"/> instance containing the unique identifier.</returns>
    public static CreateUserResponse Create(string userId)
        => new(userId);
}
