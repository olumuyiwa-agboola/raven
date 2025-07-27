using FluentValidation;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests;

/// <summary>
/// Represents the unique identifier of a user captured in the route of a request.
/// </summary>
/// <remarks>This class is used to encapsulate the unique identifier of a user, 
/// obtained from the route of a request that accepts a UserId route parameter.
/// </remarks>
public record UserIdRouteParameter
{
    [FromRoute]
    [Description("The system-assigned ID of the user.")]
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// Provides validation rules for the <see cref="UserIdRouteParameter"/> type.
/// </summary>
/// <remarks>This validator ensures that the <c>UserId</c> property of a <see cref="UserIdRouteParameter"/> 
/// instance: 
/// <list type="bullet">
///     <item><description>Is required and cannot be null or empty.</description></item> 
///     <item><description>Does not exceed a maximum length of 50 characters.</description></item>
///     <item><description>Contains only allowed characters for user IDs.</description></item> 
/// </list> 
/// Use this validator to enforce consistent validation logic for user ID route parameters.</remarks>
public class UserIdRouteParameterValidator : AbstractValidator<UserIdRouteParameter>
{
    public UserIdRouteParameterValidator()
    {
        RuleFor(x => x.UserId)
            .IsRequired()
            .MustNotExceed(50)
            .MustContainOnlyAllowedCharactersForUserIds();
    }
}
