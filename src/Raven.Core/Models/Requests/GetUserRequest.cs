using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Libraries.Enums;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests
{
    public record GetUserRequest
    {
        [FromQuery]
        public SearchType SearchType { get; init; }

        [FromQuery]
        public string SearchParameter { get; init; } = string.Empty;
    }

    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator()
        {
            RuleFor(x => x.SearchType)
                .MustBeAValidSearchType();

            When(x => x.SearchType == SearchType.UserId, () =>
            {
                RuleFor(x => x.SearchParameter)
                    .IsRequired()
                    .MustNotExceed(50)
                    .MustContainOnlyAllowedCharactersForUserIds();
            });

            When(x => x.SearchType == SearchType.PhoneNumber, () =>
            {
                RuleFor(x => x.SearchParameter)
                    .IsRequired()
                    .MustNotExceed(20)
                    .MustContainOnlyAllowedCharactersForPhoneNumbers();
            });

            When(x => x.SearchType == SearchType.EmailAddress, () =>
            {
                RuleFor(x => x.SearchParameter)
                    .IsRequired()
                    .MustNotExceed(200)
                    .MustBeAValidEmailAddress();
            });
        }
    }
}
