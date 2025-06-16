using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Libraries.Enums;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests
{
    /// <summary>
    /// Represents the query parameters for a GET request to retrieve 
    /// user information based on a specified search type and parameter.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data required to query
    /// the database for a user, obtained from the query parameters of a GET
    /// request to the relevant endpoint.</remarks>
    public record GetUserRequest
    {
        /// <summary>
        /// The <see cref="Libraries.Enums.SearchParameter"/> to be used.
        /// </summary>
        [FromQuery]
        public SearchParameter SearchParameter { get; init; }

        /// <summary>
        /// The value of the search parameter to be used.
        /// </summary>
        [FromQuery]
        public string Value { get; init; } = string.Empty;
    }

    /// <summary>
    /// Provides validation rules for <see cref="GetUserRequest"/> objects.
    /// </summary>
    /// <remarks>This validator ensures that the <see cref="GetUserRequest.SearchParameter"/> is valid and
    /// applies specific validation rules to the <see cref="GetUserRequest.Value"/> based on the selected search
    /// parameter. Supported search parameters include <see cref="SearchParameter.UserId"/> (the default value), 
    /// <see cref="SearchParameter.PhoneNumber"/>,  and <see cref="SearchParameter.EmailAddress"/>.</remarks>
    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator()
        {
            RuleFor(x => x.SearchParameter)
                .MustBeAValidSearchType();

            When(x => x.SearchParameter == SearchParameter.UserId, () =>
            {
                RuleFor(x => x.Value)
                    .IsRequired()
                    .MustNotExceed(50)
                    .MustContainOnlyAllowedCharactersForUserIds();
            });

            When(x => x.SearchParameter == SearchParameter.PhoneNumber, () =>
            {
                RuleFor(x => x.Value)
                    .IsRequired()
                    .MustNotExceed(20)
                    .MustContainOnlyAllowedCharactersForPhoneNumbers();
            });

            When(x => x.SearchParameter == SearchParameter.EmailAddress, () =>
            {
                RuleFor(x => x.Value)
                    .IsRequired()
                    .MustNotExceed(200)
                    .MustBeAValidEmailAddress();
            });
        }
    }
}
