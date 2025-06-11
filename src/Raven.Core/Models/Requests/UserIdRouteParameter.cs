using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests
{
    public class UserIdRouteParameter
    {
        [FromRoute]
        public string UserId { get; set; } = string.Empty;
    }

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
}
