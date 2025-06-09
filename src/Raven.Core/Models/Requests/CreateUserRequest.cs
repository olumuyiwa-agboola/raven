using FluentValidation;
using Raven.Core.Libraries.Constants;
using Raven.Core.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Raven.Core.Models.Requests
{
    /// <summary>
    /// Represents the body of a POST request to create a new OTP user with the specified details.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data required to create a new user obtained 
    /// from the body of a POST request to the relevant endpoint.</remarks>
    public record CreateUserRequest
    {
        /// <summary>
        /// Gets or initializes the email address associated with the user.
        /// </summary>
        public string EmailAddress { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the last name of the user.
        /// </summary>
        public string LastName { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the first name of the user.
        /// </summary>
        public string FirstName { get; init; } = string.Empty;

        /// <summary>
        /// Gets or initializes the phone number of the user.
        /// </summary>
        public string PhoneNumber { get; init; } = string.Empty;
    }

    /// <summary>
    /// Provides validation rules for the <see cref="CreateUserRequest"/> model.
    /// </summary>
    /// <remarks>This validator ensures that the required fields in a <see cref="CreateUserRequest"/> instance
    /// are populated and meet specific format requirements. The following rules are applied: <list type="bullet">
    /// <item><description><c>Email</c> must not be empty and must be a valid email address.</description></item>
    /// <item><description><c>LastName</c> must not be empty.</description></item> <item><description><c>FirstName</c>
    /// must not be empty.</description></item> <item><description><c>PhoneNumber</c> must not be
    /// empty.</description></item> </list></remarks>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
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
