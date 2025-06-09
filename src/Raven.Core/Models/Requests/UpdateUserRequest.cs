using FluentValidation;
using Raven.Core.Libraries.Constants;

namespace Raven.Core.Models.Requests
{
    public class UpdateUserRequest
    {
        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;
    }

    /// <summary>
    /// Provides validation rules for the <see cref="UpdateUserRequest"/> model.
    /// </summary>
    /// <remarks>This validator ensures that the required fields in a <see cref="UpdateUserRequest"/> instance
    /// are populated and meet specific format requirements. The following rules are applied: <list type="bullet">
    /// <item><description><c>Email</c> must not be empty and must be a valid email address.</description></item>
    /// <item><description><c>LastName</c> must not be empty.</description></item> <item><description><c>FirstName</c>
    /// must not be empty.</description></item> <item><description><c>PhoneNumber</c> must not be
    /// empty.</description></item> </list></remarks>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .IsRequired()
                .MustNotExceed(200)
                .MustBeAValidEmailAddress()
                .MustContainOnlyAllowedCharactersForEmailAddresses();

            RuleFor(x => x.LastName)
                .IsRequired()
                .MustNotExceed(100)
                .MustContainOnlyAllowedCharacters();

            RuleFor(x => x.FirstName)
                .IsRequired()
                .MustNotExceed(100)
                .MustContainOnlyAllowedCharacters();

            RuleFor(x => x.PhoneNumber)
                .IsRequired()
                .MustNotExceed(20)
                .MustContainOnlyAllowedCharactersForPhoneNumbers();
        }
    }
}
