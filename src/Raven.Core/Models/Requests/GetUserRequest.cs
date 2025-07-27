using FluentValidation;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Libraries.Enums;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests;

/// <summary>
/// Represents the query parameters for a GET request to retrieve 
/// user information based on a specified search type and parameter.
/// </summary>
/// <remarks>This class is used to encapsulate the data required to query
/// the database for a user, obtained from the query parameters of a GET
/// request to the relevant endpoint.</remarks>
public record GetUserRequest
{
    [FromQuery]
    [Description("The type of search to be performed.")]
    public SearchType SearchType { get; init; }

    [FromQuery]
    [Description("The search value to be used to query the database.")]
    public string Value { get; init; } = string.Empty;
}

/// <summary>
/// Provides validation rules for <see cref="GetUserRequest"/> objects.
/// </summary>
/// <remarks>This validator ensures that the <see cref="GetUserRequest.SearchType"/> is valid and
/// applies specific validation rules to the <see cref="GetUserRequest.Value"/> based on the selected search
/// parameter. Supported search parameters include <see cref="SearchType.UserId"/> (the default value), 
/// <see cref="SearchType.PhoneNumber"/>,  and <see cref="SearchType.EmailAddress"/>.</remarks>
public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    public GetUserRequestValidator()
    {
        RuleFor(x => x.SearchType)
            .MustBeAValidSearchType();

        RuleFor(x => x.Value)
            .IsRequired();

        When(x => !string.IsNullOrWhiteSpace(x.Value), () =>
        {
            When(x => x.SearchType == SearchType.UserId, () =>
            {
                RuleFor(x => x.Value)
                    .IsRequired()
                    .MustNotExceed(50)
                    .MustContainOnlyAllowedCharactersForUserIds();
            });

            When(x => x.SearchType == SearchType.PhoneNumber, () =>
            {
                RuleFor(x => x.Value)
                    .IsRequired()
                    .MustNotExceed(20)
                    .MustContainOnlyAllowedCharactersForPhoneNumbers();
            });

            When(x => x.SearchType == SearchType.EmailAddress, () =>
            {
                RuleFor(x => x.Value)
                    .IsRequired()
                    .MustNotExceed(200)
                    .MustBeAValidEmailAddress();
            });
        });
    }
}
